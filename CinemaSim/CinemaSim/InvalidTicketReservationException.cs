using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    public class InvalidTicketReservationException : Exception
    {
        public InvalidTicketReservationException() : base("Invalid ticket reservation.") {}
        public InvalidTicketReservationException(string message) : base(message) { }
        public InvalidTicketReservationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
