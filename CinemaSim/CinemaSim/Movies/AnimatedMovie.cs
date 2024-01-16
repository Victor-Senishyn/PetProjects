using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim.Movies
{
    public class AnimatedMovie : Movie
    {
        public string AnimationType { get; }

        public AnimatedMovie(string name, ushort releaseDate, string duration, string animationType)
            : base(name, releaseDate, duration)
                => AnimationType = animationType;
    }
}
