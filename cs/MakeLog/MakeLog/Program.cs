using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MakeLog
{
    class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Logger.Trace("Trace message");
            Logger.Debug("Debug message");
            Logger.Info("Info message");
            Logger.Warn("Warn message");
            Logger.Error("Error message");
            Logger.Fatal("Fatal message");
            Console.WriteLine("Logs added into table. Press any key to quit.");
            Console.ReadLine();
        }
    }
}
