using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class ForeignMovie : Movie
    {
        private string _language;

        public ForeignMovie(string name, ushort releaseDate, ushort timeOfTheFilm, string language) 
            : base(name, releaseDate, timeOfTheFilm)
            => _language = language;
    }
}
