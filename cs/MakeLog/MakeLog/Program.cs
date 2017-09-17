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
            Logger.Error("Error message");
            Console.ReadLine();
        }
    }
}
