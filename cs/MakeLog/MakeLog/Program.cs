using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using log4net;

namespace MakeLog
{
    class Program
    {
        private static Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly ILog LoggerForNet = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            NLogger.Trace("Trace message");
            NLogger.Debug("Debug message");
            NLogger.Info("Info message");
            NLogger.Warn("Warn message");
            NLogger.Error("Error message");
            NLogger.Fatal("Fatal message");

            log4net.Config.XmlConfigurator.Configure();
            LoggerForNet.Debug("Debug message");
            LoggerForNet.Info("Info message");
            LoggerForNet.Warn("Warn message");
            LoggerForNet.Error("Error message");
            LoggerForNet.Fatal("Fatal message");

            Console.WriteLine("Logs added into table. Press any key to quit.");
            Console.ReadLine();
        }
    }
}
