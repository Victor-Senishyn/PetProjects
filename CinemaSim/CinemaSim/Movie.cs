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
        public DateTime TimeOfTheFilm { get; }

        public Movie(string name, ushort releaseDate, string timeOfTheFilm)
            => (this.Name, this.releaseDate, this.TimeOfTheFilm) = (name, releaseDate, Convert.ToDateTime(timeOfTheFilm));

        public override string ToString() => $"Name: {Name} - {releaseDate} \nTime of the film: {TimeOfTheFilm}";
    }
}
