using DiDemo.Abstractions;
using System;

namespace DiDemo.Logging
{
    public class ConsoleLogger : ILogger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
