using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using LearningSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningSystem.Services.Implementations
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdfFromHtml(string html)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HtmlWorker(pdfDoc);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                StringReader sr = new StringReader(html);
                htmlparser.Parse(sr);
                pdfDoc.Close();

                return memoryStream.ToArray();
                //byte[] bytes = memoryStream.ToArray();
                //memoryStream.Close();
            }
        }
    }
}
