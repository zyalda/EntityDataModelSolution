using CommonLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace CommonLayer
{
    public static class LoggingService
    {
        public static void WriteToFile(this IEnumerable<ILoggable> itemsToLog)
        {
            foreach (var item in itemsToLog)
            {
                Console.WriteLine(item.Log());
            }
        }
    }
}
