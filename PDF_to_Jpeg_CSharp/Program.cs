using System;
using System.Collections.Generic;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.PdfViewer;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;



namespace PDF_to_JPG
{
    class Program
    {
        // static string file_path = @"C:\Users\markb\OneDrive - University of California, San Diego Health\Documents";
        static string[] file_paths = { @"\\ro-ariaimg-v\va_data$\HDR\Bravos\Documents", @"\\ro-ariaimg-v\va_data$\HDR\Report File Export" };
        static void write_files(string file_path, List<string> needs_writing)
        {
            foreach (string file_name in needs_writing)
            {
                Console.WriteLine($"Writing out {file_name}");
                Thread.Sleep(3000);
                string new_file_name = file_name.Substring(0, file_name.Length - 4);

                PdfViewerControl pdfViewer = new PdfViewerControl();

                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(file_name);

                pdfViewer.Load(loadedDocument);

                for (int i = 0; i < pdfViewer.PageCount; i++)
                {
                    string out_path = Path.Combine(file_path, new_file_name + $"_Page_{i}.jpeg");
                    Bitmap image = pdfViewer.ExportAsImage(i);
                    image.Save(out_path, ImageFormat.Jpeg);
                }
                loadedDocument.Dispose();
                loadedDocument = null;
            }
        }

        static void check_path(string file_path)
        {
            string[] all_files = Directory.GetFiles(file_path);
            List<string> pdf_files = new List<string>();
            List<string> jpg_files = new List<string>();

            foreach (string file_name in all_files)
            {
                if (file_name.ToLower().EndsWith(".pdf"))
                {
                    pdf_files.Add(file_name);
                }
                else if ((file_name.ToLower().EndsWith(".jpg")) || (file_name.ToLower().EndsWith(".jpeg")))
                {
                    if (file_name.Contains("_Page_"))
                    {
                        int page_index = file_name.IndexOf("_Page_");
                        jpg_files.Add(file_name.Substring(0, page_index));
                    }
                }
            }
            List<string> needs_writing = new List<string>();
            foreach (string file_name in pdf_files)
            {
                if (!jpg_files.Contains(file_name.Substring(0, file_name.Length - 4)))
                {
                    needs_writing.Add(file_name);
                }
            }
            if (needs_writing.Count > 0)
            {
                Program.write_files(file_path: file_path, needs_writing: needs_writing);
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            while (true)
            {
                Thread.Sleep(3000);
                foreach (string file_path in file_paths)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        Program.check_path(file_path: file_path);
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
