using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim.CinemaMangement
{
    public class CinemaHall
    {
        private int _countOfPlaces = 100;

        public bool[] Places { get; set; }

        public CinemaHall() => Places = Enumerable.Repeat(true, _countOfPlaces).ToArray();

        public bool IsPlaceFree(int place) => Places[place - 1];
    }
}
