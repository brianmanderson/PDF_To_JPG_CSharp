using System;
using System.Threading;
using System.IO;
using PDF_To_JPEGClass;



namespace PDF_to_JPG
{
    class Program
    {
        // static string file_path = @"C:\Users\markb\OneDrive - University of California, San Diego Health\Documents";
        static string[] file_paths = { @"\\ro-ariaimg-v\va_data$\HDR\Bravos\Documents", @"\\ro-ariaimg-v\va_data$\HDR\Report File Export" };
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            PdfToJpeg pdf_writer = new PdfToJpeg();
            while (true)
            {
                Thread.Sleep(1000);
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
