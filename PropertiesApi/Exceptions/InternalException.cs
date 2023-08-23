using System;

namespace HouseApi.Entities.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message) : base(message) { }
    }
}
