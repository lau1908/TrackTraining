using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using TrackTraining.DBModels;

namespace TrackTraining.Controllers
{
    public class HomeController : Controller
    {
        TYTEntities Database = new TYTEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();

        }
        public ActionResult Ovelser()
        {
            ViewBag.primære = Database.Ovelsers.Where(e=>e.Primære.ToLower()=="yes").ToList();

            return View();
        }

        public ActionResult Øvelser(int ovelseId)
        {
            ViewBag.ovelse = Database.Ovelsers.Where(e => e.OvelseId== ovelseId).ToList();

            return View();
        }
    }
}