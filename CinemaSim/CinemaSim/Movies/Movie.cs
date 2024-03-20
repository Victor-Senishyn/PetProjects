using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CinemaSim.Movies
{
    public class Movie
    {
        protected ushort releaseDate;
        public string Name { get; set; }
        public DateTime Duration { get; set; }

        public Movie(string name, ushort releaseDate, string duration)
            => (Name, this.releaseDate, Duration)
                = (name, releaseDate, Convert.ToDateTime(duration));

        public override string ToString() => $"{Name} - {releaseDate} Time of the film: {Duration.TimeOfDay}";
    }
}
