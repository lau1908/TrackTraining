using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using TrackTraining.DBModels;
using TrackTraining.Models;

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
            IEnumerable<Rekorder2> Leaders = Database.Rekorder2.OrderByDescending(e => e.Gentagelser);
            return View(Leaders);
        }
       
        [HttpPost]
        public ActionResult Contact(DateTime date, int Gentagelser, int Id)//bruger date og gentagelser som inputargument
        {
            string uid = User.Identity.GetUserId();//erklærer "uid" til at indholde brugerens ID

            
            AspNetUser bruger = Database.AspNetUsers.FirstOrDefault(e => e.Id == uid);

            Rekorder2 Rekord = new Rekorder2()//opretter objektet Rekord 
            {
                BrugerId = bruger.Id, //tildeler objektet Rekord 4 vaiabler
                OvelseId = Id,
                Gentagelser = Gentagelser,  
                dato = date,
            };
            Database.Rekorder2.Add(Rekord);//tilføjer den nye data til databasen
            Database.SaveChanges();//gemmer ændringer 
            return Json(Rekord); //Retunerer objektet Rekord som en Json
            
        }



            public ActionResult Ovelser(){
            ViewBag.primære = Database.Ovelsers.Where(e=>e.Primære.ToLower()!= "null").ToList();

            return View();
        }

        public ActionResult Øvelser(int ovelseId)
        {
            string uid = User.Identity.GetUserId(); //erklærer "uid" til at indholde brugerens ID
            Ovelser øvelse = Database.Ovelsers.FirstOrDefault(e => e.OvelseId == ovelseId);
            ExerciseData data = new ExerciseData(//laver et objekt af klassen ExerciseData 
                Database.Ovelsers.FirstOrDefault(e => e.OvelseId == ovelseId).Rekorder2.Where(e => e.BrugerId == uid),
                øvelse.OvelseNavn,
                øvelse.OvelseId
            );
            return View(data);
        }
    }
}