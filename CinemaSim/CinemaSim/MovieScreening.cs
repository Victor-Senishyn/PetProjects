using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class MovieScreening
    {
        public DateTime StartTime { get; set; }
        public Movie Screening { get; set; }

        public MovieScreening(DateTime startTime, Movie screening) 
            => (StartTime, Screening) = (startTime, screening);
    }
}
