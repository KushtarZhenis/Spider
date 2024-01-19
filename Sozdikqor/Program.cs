using COMMON;
using System;
using System.IO;
using iTextSharp;
using iTextSharp.awt;
using iTextSharp.awt.geom;
using iTextSharp.testutils;
using iTextSharp.text.pdf;


// string filePath = "/Users/kushtar/Desktop/SpiderSources/Sozdikqor/sozdik.json";
// string filePath = "/Users/kushtar/Desktop/SpiderSources/Sozdikqor/sozdik.txt";
// string readFilePath = "/Users/kushtar/Desktop/SpiderSources/Sozdikqor/sozdik.docx";
string readFilePath = "/Users/kushtar/Desktop/SpiderSources/Sozdikqor/sozdik.pdf";

string content = File.ReadAllText(readFilePath);
Console.WriteLine(content);
// File.WriteAllText(filePath, content);
using (PdfReader pdfReader = new PdfReader(readFilePath))
{
    // Get the number of pages in the PDF
    int pageCount = pdfReader.NumberOfPages;
    Console.WriteLine($"Number of pages: {pageCount}");

    // Read content from each page
    for (int pageNum = 1; pageNum <= pageCount; pageNum++)
    {
        string pageText = PdfTextExtractor.GetTextFromPage(pdfReader.GetPage(pageNum));
        Console.WriteLine($"Page {pageNum} text:\n{pageText}");
    }
}
