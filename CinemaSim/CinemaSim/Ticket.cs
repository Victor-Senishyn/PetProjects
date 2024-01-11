using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class Ticket
    {
        private Movie _movie;
        private DateTime _time;

        public Ticket(Movie movie, DateTime time) => (_movie, _time) = (movie, time);
        
        public override string ToString() => $"Your movie is {_movie} \nwill start at {_time}";
    }
}
