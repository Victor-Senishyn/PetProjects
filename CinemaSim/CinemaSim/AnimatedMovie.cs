using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class AnimatedMovie : Movie
    {
        private string _animationType;

        public AnimatedMovie(string name, ushort releaseDate, ushort timeOfTheFilm, string animationType) 
            : base(name, releaseDate, timeOfTheFilm)
            => _animationType = animationType;
    }
}
