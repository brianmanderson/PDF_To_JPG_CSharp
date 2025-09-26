using System;
using System.Collections.Generic;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.PdfViewer;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

namespace PDF_To_JPEGClass
{
    public class PdfToJpeg
    {
        private PdfViewerControl pdfViewer = new PdfViewerControl();
        public PdfToJpeg()
        {
        }
        public void EvaluateDirectory(string directory)
        {
            List<string> pdf_files = new List<string>();
            List<string> jpg_files = new List<string>();
            string[] all_files = Directory.GetFiles(directory);
            foreach (string file in all_files)
            {
                if (file.ToLower().EndsWith(".pdf"))
                {
                    pdf_files.Add(file);
                }
                else if ((file.ToLower().EndsWith(".jpg")) || (file.ToLower().EndsWith(".jpeg")))
                {
                    if (file.Contains("_Page_"))
                    {
                        int page_index = file.IndexOf("_Page_");
                        jpg_files.Add(file.Substring(0, page_index));
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
            foreach (string pdf_file in needs_writing)
            {
                WriteJpeg(pdf_file);
            }
        }
        public void WriteJpeg(string file_name)
        {
            string file_path = Path.GetDirectoryName(file_name);
            Console.WriteLine($"Writing out {file_name}");
            Thread.Sleep(1000);
            string new_file_name = file_name.Substring(0, file_name.Length - 4);

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
}
