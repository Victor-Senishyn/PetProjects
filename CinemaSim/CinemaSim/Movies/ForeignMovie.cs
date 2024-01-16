using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim.Movies
{
    public class ForeignMovie : Movie
    {
        public string Language;

        public ForeignMovie(string name, ushort releaseDate, string duration, string language)
            : base(name, releaseDate, duration)
                => Language = language;
    }
}
