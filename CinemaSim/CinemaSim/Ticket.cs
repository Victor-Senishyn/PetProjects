using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CinemaSim.Movies;

namespace CinemaSim
{
    public class Ticket
    {
        private Movie _movieForSession;
        private DateTime _date;
        private int _place;

        public decimal Price { get; set; } = 150.20m;

        public Ticket(Movie movie, DateTime date, int place) => (_movieForSession, _date, _place) = (movie, date, place);
        
        public override string ToString() => $"Your movie is {_movieForSession} | Your place is {_place} | Movie will start at {_date}\n";
    }
}
