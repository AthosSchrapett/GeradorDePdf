﻿using GeradorDePDF.Domain.Models.Requests;
using Microsoft.AspNetCore.Http;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace GeradorDePDF.Application.Helpers
{
    public class PdfManipulatorHelper
    {
        public static List<string> SepararPdf(PdfRequestModel pdfRequestModel)
        {
            List<string> caminhos = new();
            int contador = 0;

            foreach (var paginas in pdfRequestModel.Paginas)
            {
                PdfDocument novoDocumento = new();

                IEnumerable<int> paginasPdf = paginas.Split(',').Select(x => int.Parse(x)).AsEnumerable();

                using PdfDocument document = DefinirPaginasIncluidas(pdfRequestModel.File, paginasPdf, ref novoDocumento);
                string caminho = Path.Combine(Path.GetTempPath(), $"arquivo_{contador++}.pdf");

                caminhos.Add(caminho);
                novoDocumento.Save(caminho);
            }

            return caminhos;
        }

        public static string JuntarPdf(IEnumerable<IFormFile> files, Dictionary<int, IEnumerable<int>> paginasPdf)
        {
            
            PdfDocument novoDocumento = new();

            int contador = 1;

            foreach (IFormFile file in files)
            {
                IEnumerable<int> paginas = paginasPdf[contador];

                using PdfDocument document = DefinirPaginasIncluidas(file, paginas, ref novoDocumento);
            }

            string caminho = Path.Combine(Path.GetTempPath(), $"document.pdf");
            novoDocumento.Save(caminho);

            return caminho;
        }

        private static PdfDocument DefinirPaginasIncluidas(IFormFile file, IEnumerable<int> paginasPdf, ref PdfDocument pdfDocument)
        {
            PdfDocument document = PdfReader.Open(file.OpenReadStream(), PdfDocumentOpenMode.Import);

            foreach (var pagina in paginasPdf)
            {
                pdfDocument.AddPage(document.Pages[pagina]);
            }

            return document;
        }
    }
}
