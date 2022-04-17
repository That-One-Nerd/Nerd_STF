using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Exceptions
{
    [Serializable]
    public class Nerd_STFException : Exception
    {
        public Nerd_STFException() { }
        public Nerd_STFException(string message) : base(message) { }
        public Nerd_STFException(string message, Exception inner) : base(message, inner) { }
        protected Nerd_STFException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
