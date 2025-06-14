using System;
using System.IO;
using System.Text.RegularExpressions;
//using TheArtOfDev.HtmlRenderer;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Web;
using PdfSharp.Pdf;
using PdfSharp;


#if NETCOREAPP
	using Microsoft.AspNetCore.Http;
#endif

using FSNetwork;

namespace FSPdf
{
	/// <summary>
	/// Description of CreatePDF.
	/// </summary>
	public class CreatePDF
	{
		public CreatePDF()
		{
		}


#if NETFRAMEWORK
        public static void Generate(HttpContext context, string html, bool landscape)
#else
		public static void Generate(Microsoft.AspNetCore.Http.HttpContext context, string html, bool landscape, string fileName)
#endif
        {
			PdfGenerateConfig pdfConfig = new PdfGenerateConfig();
        	pdfConfig.PageSize = PdfSharp.PageSize.A4;
        	pdfConfig.MarginBottom = 25;
        	pdfConfig.MarginLeft = 25;
        	pdfConfig.MarginRight = 25;
        	pdfConfig.MarginTop = 25;

#if NETCOREAPP
			if(landscape)
				pdfConfig.PageOrientation = PageOrientation.Landscape;
#endif
			
			html = Web.ReplaceUrlRes(html);
			html = FSLibrary.TextUtil.RemoveLinks(html);

			//string tmpFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
			//doc.Save(tmpFile);

			using (MemoryStream ms = new MemoryStream())
			{
				PdfDocument doc = PdfGenerator.GeneratePdf(html, pdfConfig, null, null, null);
				doc.Save(ms);

#if NETFRAMEWORK
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-length", ms.ToArray().Length.ToString());
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.Flush();
                context.Response.Close();
#else
				context.Response.SendFileAsync(fileName);
#endif
			}

			//            context.Response.AppendHeader("Content-Disposition", 
			//                                          "inline; filename=" + tmpFile);

		}
	}
}