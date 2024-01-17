using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim.CustomExceptions
{
    public class SeatAlreadyTakenException : Exception
    {
        public SeatAlreadyTakenException() : base("The seat is already taken.") { }
        public SeatAlreadyTakenException(string message) : base(message) { }
        public SeatAlreadyTakenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
