using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class History : IHistory
    {

        private string filePath = @"C:\Users\Decagon\Documents\DECAGON\C#\PROJECTS\archive.txt";

        private ILoggerManager _logger;

        public History(ILoggerManager logger)
        {
            _logger = logger;
        }
        public void Archive(string output)
        {
            try
            {                

                if (File.Exists(filePath))
                {
                    File.AppendAllText(filePath, output + "\n");
                }
                else File.WriteAllText(filePath, output + "\n");

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //Console.WriteLine(ex.Message);
            }
        }

        public async Task<string[]> RetrieveAsync()
        {

            
            try
            {
                if (File.Exists(filePath))
                {
                    var lines = (await File.ReadAllLinesAsync(filePath));

                    return lines; // Console.WriteLine(line);
                }
                else Console.WriteLine("\n                              Archive is currently empty...");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                Console.WriteLine(ex.Message);
            }
            
            return new string[] { };

        }
    }
}
