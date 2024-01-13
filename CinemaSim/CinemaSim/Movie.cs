using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class Movie
    {
        protected ushort releaseDate;
        public string Name { get; }
        public DateTime Duration { get; }

        public Movie(string name, ushort releaseDate, string duration)
            => (this.Name, this.releaseDate, this.Duration) 
                = (name, releaseDate, Convert.ToDateTime(duration));

        public override string ToString() => $"Name: {Name} - {releaseDate} \nTime of the film: {Duration.TimeOfDay}";
    }
}
