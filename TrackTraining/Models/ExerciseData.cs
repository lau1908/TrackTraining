using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTraining.Models
{
    public class ExerciseData
    {
        public IEnumerable<ExerciseDataPoint> data { get; set; }
        public ExerciseData(IEnumerable<ExerciseDataPoint> data) 
        {
            this.data = data;
        }
    }

    public class ExerciseDataPoint
    {
        public int reps { get; set; }
        public DateTime time { get; set; }
        public ExerciseDataPoint(int reps, DateTime time)
        {
            this.reps = reps;
            this.time = time;
        }
    }
}