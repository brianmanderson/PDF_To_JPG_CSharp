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
        static string[] file_paths = { @"C:\Users\markb\Downloads\Test", @"\\ro-ariaimg-v\va_data$\HDR\Bravos\Documents", @"\\ro-ariaimg-v\va_data$\HDR\Report File Export" };
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            PdfToJpeg pdf_writer = new PdfToJpeg();
            while (true)
            {
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
