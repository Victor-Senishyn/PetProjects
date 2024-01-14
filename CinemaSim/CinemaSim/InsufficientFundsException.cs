using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSim
{
    internal class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient Funds on the Balance.") { }
        public InsufficientFundsException(string message) : base(message) { }
        public InsufficientFundsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
