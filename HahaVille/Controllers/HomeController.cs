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
            List<Game> listOfGames = listOfCategory[0].Games.ToList();
            List<User> listOfUsers = db.Users.ToList();
            return View(listOfGames);
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