using Fruitables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fruitables.Controllers
{
    public class HomeController : Controller
    {
        private fruitablesEntities5 db = new fruitablesEntities5();

        public ActionResult Index()
        {
            var products = db.products.ToList();
            return View(products);
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