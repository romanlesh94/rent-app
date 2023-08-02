using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CredentialsExc : Exception
    {
        public CredentialsExc()
        {

        }

        public CredentialsExc(string message) : base(message)
        {

        }

        public CredentialsExc(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
