using System.Text;

namespace Nerd_STF
{
    public class Logger
    {
        public event Action<LogMessage> OnMessageRecieved = DefaultLogHandler;

        public LogMessage[] Cache => msgs.ToArray();
        public int CacheSize = 64;
        public List<LogSeverity> IncludeSeverities = new()
        {
            LogSeverity.Warning,
            LogSeverity.Error,
            LogSeverity.Fatal,
        };
        public Stream? LogStream;
        public int WriteSize;

        private readonly List<LogMessage> msgs;
        private readonly List<string> writeCache;

        public Logger(Stream? logStream = null, int cacheSize = 64, int writeSize = 1)
        {
            CacheSize = cacheSize;
            LogStream = logStream;
            WriteSize = writeSize;

            msgs = new(CacheSize);
            writeCache = new();
        }

        public void Send(LogMessage msg)
        {
            if (!IncludeSeverities.Contains(msg.Severity)) return;

            msgs.Insert(0, msg);
            writeCache.Add(msg.ToString());
            while (msgs.Count > CacheSize) msgs.RemoveAt(CacheSize);
            OnMessageRecieved(msg);

            if (writeCache.Count >= WriteSize && LogStream != null)
            {
                string s = "";
                foreach (string cache in writeCache) s += cache + "\n" + (cache.Contains('\n') ? "\n" : "");
                LogStream.Write(Encoding.Default.GetBytes(s));
                LogStream.Flush();
                writeCache.Clear();
            }
        }

        public static void DefaultLogHandler(LogMessage msg)
        {
            ConsoleColor color = msg.Severity switch
            {
                LogSeverity.Debug => ConsoleColor.DarkGray,
                LogSeverity.Information => ConsoleColor.White,
                LogSeverity.Warning => ConsoleColor.DarkYellow,
                LogSeverity.Error => ConsoleColor.Red,
                LogSeverity.Fatal => ConsoleColor.DarkRed,
                _ => throw new Exception("Unknown log severity " + msg.Severity),
            };

            ConsoleColor originalCol = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(msg.ToString());

            Console.ForegroundColor = originalCol;
        }
    }
}
