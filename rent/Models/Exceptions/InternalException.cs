using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.Entities.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message) : base(message) { }
    }
}
