using System;

namespace Nerd_STF.Exceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException() : base("Invalid polynomial order.") { }
        public InvalidOrderException(string message) : base(message) { }
    }
}
