using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace VideoConverter
{
    public class Logging
    {
        private static readonly string file = $@"{Directory.GetCurrentDirectory()}/Logs/{DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss", CultureInfo.InvariantCulture)}.txt";
        public Logging()
        {
            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/Logs"))
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Logs");
        }

        public static void WriteLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8))  // If the file doesn't exist, StreamWriter constructor will create it. 
                sw.WriteLineAsync($"{DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss", CultureInfo.InvariantCulture)} -- {msg}");            
        }
    }
}
