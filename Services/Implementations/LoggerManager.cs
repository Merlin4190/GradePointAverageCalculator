using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.Implementations
{
    public class LoggerManager : ILoggerManager
    {
        private string filePath = $@"C:\Users\Decagon\Documents\DECAGON\C#\PROJECTS\{DateTime.Now.Date}_log.txt";

        public void LogError(string message)
        {
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, $"{DateTime.Now.Date}{DateTime.Now.TimeOfDay}" +  message + "\n");
            }
            else File.WriteAllText(filePath, $"{DateTime.Now.Date}{DateTime.Now.TimeOfDay}" +  message  + "\n");
        }
    }
}
