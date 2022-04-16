using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackTraining.DBModels;

namespace TrackTraining.Models
{
    public class LeaderboardData
    {
        public IEnumerable<Rekorder2> data { get; set; }
        public IEnumerable<Ovelser> Øvelser { get; set; }
        public IEnumerable<Rekorder2> gentagelser { get; set; }
        //public IOrderedQueryable<Rekorder2> Rekorder2s { get; }
        //public IQueryable<Ovelser> Ovelsers { get; }

        public LeaderboardData(IEnumerable<Rekorder2> data, IEnumerable<Ovelser> Øvelser)
        {
            this.data = data;
            this.Øvelser = Øvelser;
        }

        //public LeaderboardData(IOrderedQueryable<Rekorder2> rekorder2s, IQueryable<Ovelser> ovelsers)
        //{
        //    Rekorder2s = rekorder2s;
        //    Ovelsers = ovelsers;
        //}
    }
    
}