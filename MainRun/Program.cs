using PDF_To_JPEGClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainRun
{
    internal class Program
    {
        // static string file_path = @"C:\Users\markb\OneDrive - University of California, San Diego Health\Documents";
        static List<string> default_file_paths = new List<string> { @"C:\Users\markb\Downloads\Test", @"\\ro-ariaimg-v\va_data$\HDR\Bravos\Documents", @"\\ro-ariaimg-v\va_data$\HDR\Report File Export" };
        static string file_paths_file = Path.Combine(".", $"FilePaths.txt");
        static void update_file_paths()
        {
            List<string> file_paths = new List<string> { };
            foreach (string file_path in default_file_paths)
            {
                file_paths.Add(file_path);
            }
            if (!File.Exists(file_paths_file))
            {
                try
                {
                    StreamWriter fid = new StreamWriter(file_paths_file);
                    foreach (string file_path in default_file_paths)
                    {
                        fid.WriteLine($"{file_path}");
                    }
                    fid.Close();
                }
                catch
                {
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            PdfToJpeg pdf_writer = new PdfToJpeg();
            while (true)
            {
                update_file_paths();
                List<string> file_paths = new List<string> { };
                try
                {
                    string all_file_paths = File.ReadAllText(file_paths_file);
                    foreach (string file_path in all_file_paths.Split('\n'))
                    {
                        string file = file_path.Split('\r')[0];
                        if (!file_paths.Contains(file))
                        {
                            file_paths.Add(file);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Couldn't read the FilePaths.txt file...");
                    Thread.Sleep(3000);
                }
                Thread.Sleep(3000);
                foreach (string file_path in file_paths)
                {
                    if (!Directory.Exists(file_path))
                    {
                        continue;
                    }
                    Thread.Sleep(1000);
                    try
                    {
                        pdf_writer.EvaluateDirectory(file_path);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}
