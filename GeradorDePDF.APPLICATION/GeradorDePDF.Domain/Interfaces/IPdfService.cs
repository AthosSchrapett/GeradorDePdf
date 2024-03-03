﻿using GeradorDePDF.Domain.Models;
using GeradorDePDF.Domain.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace GeradorDePDF.Domain.Services.Interfaces;

public interface IPdfService
{
    public MemoryStream GeraPdf(IFormFile file);
    public MemoryStream GeraPdf(ModelPdf model);
    public MemoryStream SplitPdf(Models.Requests.PdfRequestModel model);
    public MemoryStream JoinPdf(IEnumerable<IFormFile> files, List<string> paginas);
    public MemoryStream GeraPdfWithCsv(CsvPdfRequestModel model);
}
