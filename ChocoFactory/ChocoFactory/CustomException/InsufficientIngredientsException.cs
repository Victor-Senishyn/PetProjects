using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoFactory.CustomException
{
    public class InsufficientIngredientsException : Exception
    {
        public InsufficientIngredientsException() { }

        public InsufficientIngredientsException(string message) : base(message) { }

        public InsufficientIngredientsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
