using DinkToPdf;
using DinkToPdf.Contracts;
using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models;

using FastTripApp.DAO.Models.Reports;
using FastTripApp.DAO.Models.Reports.Pdf;
using FastTripApp.DAO.Models.Reports.Pdf.Default;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FastTripApp.BL.Services
{
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;
        private readonly IUtilService _utilService;
        private readonly IViewRenderService _viewRenderService;

        public ReportService(IConverter converter,

            IUtilService utilService,
            IViewRenderService viewRenderService)
        {
            _converter = converter;
            _utilService = utilService;

            _viewRenderService = viewRenderService;
        }

        public async Task<CustomPdf> GetPdfReportAsync<T>(T model, string viewPathRender)
        {
            var htmlData = await GetHtmlContentAsync(model, viewPathRender);
            var htmlToPdfDocument = GetHtmlToPdfDocument(htmlData);

            var customPdf = new CustomPdf()
            {
                FileName = GetPfdFileName(),
                FileBytes = _converter.Convert(htmlToPdfDocument)
            };
            return customPdf;
        }



        public async Task<string> GetHtmlContentAsync<T>(T model, string viewPathRender)
        {
            if (model == null)
            {
                return "Default null content report";
            }

            return await _viewRenderService.RenderToStringAsync(viewPathRender, model);
        }

        private HtmlToPdfDocument GetHtmlToPdfDocument(string htmlContent)
        {
            return new DefaultPdf(htmlContent);
        }

        private string GetPfdFileName()
        {
            var date = _utilService.GetDateTimeNow();
            var time = $"{date:hh}-{date:mm}-{date:ss}";
            var fileName = $"{date:d} {time}.pdf";

            return fileName;
        }


    }
}
