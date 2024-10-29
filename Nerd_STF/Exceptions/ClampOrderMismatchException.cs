using System;

namespace Nerd_STF.Exceptions
{
    public class ClampOrderMismatchException : Exception
    {
        public ClampOrderMismatchException() : base("Minimum is greater than maximum.") { }
        public ClampOrderMismatchException(string minName, string maxName) : base($"'{minName}' is greater than '{maxName}'") { }
    }
}
