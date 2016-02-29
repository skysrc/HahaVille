using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HahaVille.Models;
using HahaVille.DAL;

namespace HahaVille.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Details(string name)
        {
            //int nGameId = 0;
            GameInfo objGI = null;
            //if (int.TryParse(name, out nGameId))
            //{
                HahaVilleContext db = new HahaVilleContext();
                var objTargetGame = (from g in db.Games
                                     where g.Name == name.Replace("-"," ")
                                     select new
                                     {
                                         Id = g.Id,
                                         Thumbnail = g.Thumbnail,
                                         Uri = g.GamePath,
                                         CategoryId = g.CategoryId,
                                         Props = g.LocalizedProperties,
                                     }).FirstOrDefault();

                if (objTargetGame != null)
                {
                    objGI = new GameInfo()
                    {
                        Id = objTargetGame.Id,
                        LanguageId = 1,
                        CategoryId = objTargetGame.CategoryId,
                        Thumbnail = objTargetGame.Thumbnail,
                        Uri = objTargetGame.Uri
                    };

                    if (objTargetGame.Props != null && objTargetGame.Props.Count > 0)
                    {
                        LocalizedProperty lpName = objTargetGame.Props.Where(x => x.LocaleKey.Equals("game.name")).FirstOrDefault();
                        LocalizedProperty lpTitle = objTargetGame.Props.Where(x => x.LocaleKey.Equals("game.metatitle")).FirstOrDefault();
                        LocalizedProperty lpDesc = objTargetGame.Props.Where(x => x.LocaleKey.Equals("game.desc")).FirstOrDefault();
                        LocalizedProperty lpKeyword = objTargetGame.Props.Where(x => x.LocaleKey.Equals("game.metakeyword")).FirstOrDefault();
                        LocalizedProperty lpCatName = objTargetGame.Props.Where(x => x.LocaleKey.Equals("category.name")).FirstOrDefault();

                        objGI.Name = lpName != null ? lpName.LocaleValue : string.Empty;
                        objGI.Title = lpTitle != null ? lpTitle.LocaleValue : string.Empty;
                        objGI.Description = lpDesc != null ? lpDesc.LocaleValue : string.Empty;
                        objGI.Keyword = lpKeyword != null ? lpKeyword.LocaleValue : string.Empty;
                        objGI.CategoryName = lpCatName != null ? lpCatName.LocaleValue : string.Empty;

                    }
                }
            //}

            if (objGI != null)
            {
                return View(objGI);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Play(string name)
        {
            int nGameId = 0;
            Game objGame = null;
            //if (int.TryParse(name, out nGameId))
            //{
                HahaVilleContext db = new HahaVilleContext();
                objGame = (from g in db.Games where g.Name == name.Replace("-"," ") select g).FirstOrDefault();
            //}

            if (objGame != null)
            {
                return View(objGame);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
