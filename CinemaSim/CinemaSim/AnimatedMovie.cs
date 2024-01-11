using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class AnimatedMovie : Movie
    {
        public string AnimationType { get; }

        public AnimatedMovie(string name, ushort releaseDate, string timeOfTheFilm, string animationType) 
            : base(name, releaseDate, timeOfTheFilm)
            => AnimationType = animationType;
    }
}
