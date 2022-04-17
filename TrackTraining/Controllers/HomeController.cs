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
           


            LeaderboardData data = new LeaderboardData(

                Database.Rekorder2.OrderByDescending(e => e.Gentagelser), //sætter den fra højst til laveste gentagelset

               Database.Ovelsers.Where(e => e.Primære.ToLower() == "yes") //finder alle dem som er primære

            ) ;

         
            return View(data);
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
            Database.Rekorder2.Add(Rekord);//tilføjer den nye data 
            Database.SaveChanges();//gemmer ændringer 
            return Json(Rekord);

       

        }



            public ActionResult Ovelser()
        {
            ViewBag.primære = Database.Ovelsers.Where(e=>e.Primære.ToLower()!= "null").ToList(); //finder alle de primære øvelser, og putter dem i en viewbag

            return View();
        }

        public ActionResult Øvelser(int ovelseId)
        {
            string uid = User.Identity.GetUserId(); //erklærer "uid" til at indholde brugerens ID
            Ovelser øvelse = Database.Ovelsers.FirstOrDefault(e => e.OvelseId == ovelseId); //finder øvelsen der matcher id'et
            ExerciseData data = new ExerciseData(
                Database.Ovelsers.FirstOrDefault(e => e.OvelseId == ovelseId).Rekorder2.OrderBy(e => e.dato).Where(e=> e.BrugerId == uid),
                øvelse.OvelseNavn,
                øvelse.OvelseId
            );
              
         return View(data);
        }
        public ActionResult Søg()
        {
            return View(Database.Ovelsers); //indeholder alle øvelserne
        }
        [HttpGet]
        public PartialViewResult ShowData (string Searchvalue) //tager en string SearchValue som paramet
        {

            IEnumerable<Ovelser> Øvvelser = Database.Ovelsers.Where(e => e.OvelseNavn.Contains(Searchvalue)).AsEnumerable(); //laver en liste med øvelser, som indeholder searchvalue i deres navn
            return PartialView("ShowData", Øvvelser); //returner et partialview: "showdata", og sender listen med øvelserne med
        }
    }
}