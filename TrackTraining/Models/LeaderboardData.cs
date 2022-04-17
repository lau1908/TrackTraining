using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackTraining.DBModels;

namespace TrackTraining.Models
{
    public class LeaderboardData ////ExerciseData er en klasse som er lavet for at struktrurerer dataen så vi kan tilgå den under "About.cshtml"
    {
        public IEnumerable<Rekorder2> data { get; set; }
        public IEnumerable<Ovelser> Øvelser { get; set; }
        
       

        public LeaderboardData(IEnumerable<Rekorder2> data, IEnumerable<Ovelser> Øvelser)
        {
            this.data = data;
            this.Øvelser = Øvelser;
        }

    }
    
}