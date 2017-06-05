using Log4N.Configuration;
using Log4N.Logger.Specific;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4N.Logger
{
    public static class Log
    {
        private static ISpecificLog _specificLog;

        static Log()
        {
            LogSection logSection = ConfigurationManager.GetSection("log") as LogSection;
            switch(logSection.Target)
            {
                case LogTarget.CONSOLE:
                    _specificLog = new ConsoleLog();
                    break;
                case LogTarget.FILE:
                    _specificLog = new FileLog(logSection.File);
                    break;
            }
        }

        public static void Error(string message)
        {
            _specificLog.Write(CreateMessage(LogType.ERROR, message));
        }

        public static void Info(string message)
        {
            _specificLog.Write(CreateMessage(LogType.INFO, message));
        }

        public static void Warning(string message)
        {
            _specificLog.Write(CreateMessage(LogType.WARNING, message));
        }

        public static void Exit()
        {
            _specificLog.Write(CreateMessage(LogType.INFO, "EndLogging"));
            _specificLog.Dispose();
        }

        private static string CreateMessage(LogType logType, string message)
        {
            return string.Format("[{0}] - {1} : {2}", logType.ToString(), DateTime.Now.ToString("dd.MM.yyyy hh:mm"), message);
        }
    }
}
