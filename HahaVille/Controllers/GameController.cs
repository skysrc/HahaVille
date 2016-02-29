﻿using System;
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

        public ActionResult Category(string name)
        {

            HahaVilleContext db = new HahaVilleContext();
            List<GameInfo> listOfGameInfo = (from c in db.Category
                                             from g in c.Games
                                             where c.Name == name
                                             select new GameInfo
                                             {
                                                 Id = g.Id,
                                                 LanguageId = 1,
                                                 Thumbnail = g.Thumbnail,
                                                 Uri = g.GamePath,
                                                 CategoryId = c.Id,
                                             }).ToList();

            if (listOfGameInfo.Count > 0)
            {
                int[] arrOfGameIds = listOfGameInfo.Select(x => x.Id).ToArray();

                List<LocalizedProperty> listOfLocalizedProps = (from props in db.LocalizedProperties
                                                                where arrOfGameIds.Contains(props.EntityId) &&
                                                                      props.LanguageId == 1
                                                                select props).ToList();

                foreach (var objGI in listOfGameInfo)
                {
                    var prop = listOfLocalizedProps.Where(x => x.EntityId == objGI.Id);
                    foreach (var p in prop)
                    {
                        switch (p.LocaleKey)
                        {
                            case "game.name":
                                objGI.Name = p.LocaleValue;
                                break;
                            case "game.metatitle":
                                objGI.Title = p.LocaleValue;
                                break;
                            case "game.desc":
                                objGI.Description = StringHtmlExtensions.TruncateHtml(p.LocaleValue, 90, "... <br /> <a href=\"/games/" + objGI.Name.Replace(" ","-") + "\" >(View Details)</a>");
                                break;
                            case "game.metakeyword":
                                objGI.Keyword = p.LocaleValue;
                                break;
                            case "category.name":
                                objGI.CategoryName = p.LocaleValue;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
           
            return View(listOfGameInfo);

        }
    }
}
