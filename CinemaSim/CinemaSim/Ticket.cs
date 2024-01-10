using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class Ticket
    {
        private Movie _movie;
        private string _time;

        public Ticket(Movie movie, string time) => (_movie, _time) = (movie, time);
        
        public override string ToString() => $"Your movie is {_movie} \nwill start at {_time}";
    }
}
