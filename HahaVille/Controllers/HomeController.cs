using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HahaVille.Models;
using HahaVille.DAL;

namespace HahaVille.Controllers
{
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
                        objGI.Description = lpDesc != null ? lpDesc.LocaleValue : string.Empty;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}