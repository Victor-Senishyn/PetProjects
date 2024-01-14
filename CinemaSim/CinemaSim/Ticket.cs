using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CinemaSim
{
    public class Ticket
    {
        private Movie _movie;
        private DateTime _time;
        private int _place;
        [XmlIgnore]
        public decimal Price { get; set; } = 150.20m;
        public Ticket(){}
        public Ticket(Movie movie, DateTime time, int place) => (_movie, _time, _place) = (movie, time, place);
        
        public override string ToString() => $"Your movie is {_movie} | Your place is {_place} | Movie will start at {_time}\n";
    }
}
