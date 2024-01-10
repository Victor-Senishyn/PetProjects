using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class Movie
    {
        protected ushort releaseDate;
        public string Name { get; }
        public ushort TimeOfTheFilm { get; }
        public string FormatForTime => (TimeSpan.FromMinutes(TimeOfTheFilm)).ToString(@"hh\:mm");

        public Movie(string name, ushort releaseDate, ushort timeOfTheFilm)
            => (this.Name, this.releaseDate, this.TimeOfTheFilm) = (name, releaseDate, timeOfTheFilm);

        public override string ToString() => $"Name: {Name} - {releaseDate} \nTime of the film: {FormatForTime}";
    }
}
