using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class ForeignMovie : Movie
    {
        public string Language;

        public ForeignMovie(string name, ushort releaseDate, string timeOfTheFilm, string language) 
            : base(name, releaseDate, timeOfTheFilm)
            => Language = language;
    }
}
