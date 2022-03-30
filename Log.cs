using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStateEmail
{
    internal class Log
    {
        public static void writeLog(string text)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string path = "C:\\tareas\\logTareas";
            string file = $"logServerState{DateTime.Now.ToString("yyMMdd")}.log";
            if (Directory.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path + "\\" + file))
                {
                    sw.WriteLine($"INFO {date}: {text}");
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine($"Directory {di.FullName} was created successfully");
                using (StreamWriter sw = File.AppendText(path + "\\" + file))
                {
                    sw.WriteLine($"INFO {date}: {text}");
                }
            }
        }
    }
}
