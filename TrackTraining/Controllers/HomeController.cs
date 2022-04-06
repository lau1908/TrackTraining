using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        [HttpPost]
        public ActionResult Contact(DateTime date, int Gentagelser)
        {
            string uid = User.Identity.GetUserId();//erklærer "uid" til at indholde brugerens ID

            AspNetUser bruger = Database.AspNetUsers.FirstOrDefault(e => e.Id == uid);
            Rekorder2 ovl = Database.Rekorder2.FirstOrDefault(i => i.BrugerId == uid);

            Rekorder2 Rekord = new Rekorder2()//opretter objektet Rekord 
            {
                BrugerId = bruger.Id, //tildeler objektet Rekord 4 vaiabler
                OvelseId = ovl.OvelseId,
                Gentagelser = Gentagelser,  
                dato = date,
            };
            Database.Rekorder2.Add(Rekord);//tilføjer den nye data 
            Database.SaveChanges();//gemmer ændringer 
            return Json(Rekord); 
            
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