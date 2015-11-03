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
            int nGameId = 0;
            List<Game> listOfGames = null;
            if (int.TryParse(name, out nGameId))
            {
                HahaVilleContext db = new HahaVilleContext();
                listOfGames = (from g in db.Games where g.Id == nGameId select g).ToList();
            }

            if (listOfGames != null && listOfGames.Count() > 0)
            {
                return View(listOfGames[0]);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Play(string name)
        {
            int nGameId = 0;
            List<Game> listOfGames = null;
            if (int.TryParse(name, out nGameId))
            {
                HahaVilleContext db = new HahaVilleContext();
                listOfGames = (from g in db.Games where g.Id == nGameId select g).ToList();
            }

            if (listOfGames != null && listOfGames.Count() > 0)
            {
                return View(listOfGames[0]);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }
    }
}
