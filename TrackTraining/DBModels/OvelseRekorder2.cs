using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTraining.DBModels
{
    public class OvelseRekorder2
    {

        public string OvelseNavn { get; set; }
        public string Billede { get; set; }
        public int OvelseId { get; set; }
        public DateTime dato { get; set; }
        public int gentagelser { get; set; }
       



    }
}