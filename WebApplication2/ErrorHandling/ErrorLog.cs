using System.Diagnostics;

namespace WebApplication2.ErrorHandling
{
    public class ErrorLog
    {
        public static void save(string logMessage, TextWriter w)
        {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
          
        }
    }
}
