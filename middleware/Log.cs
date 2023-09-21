using System;
using System.Configuration;
using System.IO;

namespace middleware
{
    public static class Log
    {
        public static void Write(string message)
        {
            string path = ConfigurationManager.AppSettings["log"]?.ToString();
            if (string.IsNullOrEmpty(path)) return;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fileName = $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}_log.txt";
            try
            {
                using (var w = File.AppendText(Path.Combine(path, fileName)))
                {
                    w.Write(Environment.NewLine);
                    w.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.Write("\t");
                    w.Write("> {0}", message);
                }
            }
            catch { }
        }
    }
}
