using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HahaVille.Models;
using HahaVille.DAL;
using HahaVille.Helper;
using System.Text;

namespace HahaVille.Controllers
{
    //[RoutePrefix("")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HahaVilleContext db = new HahaVilleContext();
            List<Category> listOfCategory = db.Category.ToList();
            List<Game> listOfGames = new List<Game>();
            List<GameInfo> listOfGameInfos = new List<GameInfo>();
            foreach (var category in listOfCategory)
            {
                listOfGames.AddRange((from g in db.Games
                                      where g.Category.Id == category.Id
                                      select g).Take(4));
            }

            int[] arrOfGameIds = null;
            arrOfGameIds = listOfGames.Select(x => x.Id).ToArray();

            LocalizedProperty[] arrProps = (from props in db.LocalizedProperties
                                            where arrOfGameIds.Contains(props.EntityId) &&
                                                  props.LanguageId == 1
                                            select props).ToArray();

            foreach (var category in listOfCategory)
            {
                foreach (var game in listOfGames.Where(x => x.CategoryId == category.Id))
                {
                    LocalizedProperty[] arrGameProps = arrProps.Where(x => x.EntityId == game.Id).ToArray();
                    GameInfo objGI = new GameInfo() { Id = game.Id,
                                                      LanguageId = 1,
                                                      CategoryId = category.Id,
                                                      Thumbnail = game.Thumbnail,
                                                      Uri = game.GamePath};
                    if (arrGameProps.Length > 0)
                    {
                        LocalizedProperty lpName = arrGameProps.Where(x => x.LocaleKey.Equals("game.name")).FirstOrDefault();
                        LocalizedProperty lpTitle = arrGameProps.Where(x => x.LocaleKey.Equals("game.metatitle")).FirstOrDefault();
                        LocalizedProperty lpDesc = arrGameProps.Where(x => x.LocaleKey.Equals("game.desc")).FirstOrDefault();
                        LocalizedProperty lpKeyword = arrGameProps.Where(x => x.LocaleKey.Equals("game.metakeyword")).FirstOrDefault();
                        LocalizedProperty lpCatName = arrGameProps.Where(x => x.LocaleKey.Equals("category.name")).FirstOrDefault();

                        objGI.Name = lpName != null ? lpName.LocaleValue : string.Empty;
                        objGI.Title = lpTitle != null ? lpTitle.LocaleValue : string.Empty;
                        objGI.Description = lpDesc != null ? StringHtmlExtensions.TruncateHtml(lpDesc.LocaleValue, 90, "... <br /> <a href=\"games/" + game.Name.Replace(" ","-") + "\" >(View Details)</a>") : string.Empty;
                        objGI.Keyword = lpKeyword != null ? lpKeyword.LocaleValue : string.Empty;
                        objGI.CategoryName = lpCatName != null ? lpCatName.LocaleValue : category.Name;

                    }
                    listOfGameInfos.Add(objGI);
                }
               
            }


            //List<User> listOfUsers = db.Users.ToList();
            return View(listOfGameInfos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "HahaVille Mini Games";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult Search(string query)
        //{
        //    string someQ = query;

        //    SearchResult srResults = new SearchResult();
        //    srResults.ResultCount = 5;
        //    srResults.Keywords = query;
        //    return View("Search", "_Layout2", srResults);
        //}
        public ActionResult Search()
        {
            return View("Search", "_Layout2");
        }
         [OutputCache(Duration = 86400)]
        public ActionResult SiteMap()
        {
            var sitemapNodes = SitemapHelper.GetSitemapNodes(this.Url);
            string xml = SitemapHelper.GetSitemapDocument(sitemapNodes);
            return this.Content(xml, System.Net.Mime.MediaTypeNames.Text.Xml, Encoding.UTF8);
        }
        [OutputCache(Duration = 86400)]
        public ContentResult Robots()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /Flashgame/*");
            stringBuilder.AppendLine("disallow: /GameLogo/*");
            //stringBuilder.AppendLine("allow: /error/foo");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.RouteUrl("SiteMap", null, this.Request.Url.Scheme).TrimEnd('/'));
            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}