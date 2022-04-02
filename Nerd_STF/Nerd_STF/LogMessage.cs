using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF
{
    public struct LogMessage
    {
        public string Message;
        public LogSeverity Severity;
        public DateTime Timestamp;

        public LogMessage() : this("", LogSeverity.Information, null) { }
        public LogMessage(string msg, LogSeverity severity, DateTime? time = null)
        {
            Message = msg;
            Severity = severity;
            Timestamp = time ?? DateTime.Now;
        }

        public override string ToString() => Timestamp + " " + Severity.ToString().ToUpper() + ": " + Message;
    }
}
