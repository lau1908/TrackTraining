using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackTraining.DBModels;

namespace TrackTraining.Models
{
    public class ExerciseData//ExerciseData er en klasse som er lavet for at struktrurerer dataen så vi kan tilgå den under "Øvelser.cshtml"
    {
        public IEnumerable<Rekorder2> data { get; set; } 
        public string name { get; set; }
        public int id { get; set; }

        public ExerciseData(IEnumerable<Rekorder2> data, string name, int id) 
        {
            this.data = data;
            this.name = name;
            this.id = id;
        }
    }
}