using DinkToPdf;
using DinkToPdf.Contracts;
using FastTripApp.BL.Extensions;
using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Pdf;
using FastTripApp.DAO.Models.Reports;
using FastTripApp.DAO.Models.Reports.StylePdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FastTripApp.BL.Services
{
    public class ReportService : Controller, IReportService
    {
        private readonly IConverter _converter;
        private readonly IUtilService _utilService;
        public ReportService(IConverter converter,
            IUtilService utilService)
        {
            _converter = converter;
            _utilService = utilService;
        }
       
        public GeneralTemplate GeneralTemplate()
        {
            return new GeneralTemplate();
        }

        public CustomPdf GeneratePdfReport(Trip trip)
        {
            var html = GetDetailsTrip(trip);
            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = new DefaultGlobalSettings(),
                Objects = { new DefaultObjectSettings() { HtmlContent = html} },
            };

            var customPdf = new CustomPdf()
            {
                FileName = GetPfdFileName(trip.Name),
                FileBytes = _converter.Convert(htmlToPdfDocument)
            };
            return customPdf;
        }

        private string GetPfdFileName(string tripName)
        {
            var time = _utilService.GetDateTimeNow();
            var fileName = $"{time:d}_{tripName}.pdf";
 
            return fileName;
        }

        protected string RenderViewToString<T>(string viewPath, T model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var view = new WebFormView(ControllerContext, viewPath);
                var vdd = new ViewDataDictionary<T>(model);
                var viewCxt = new ViewContext(ControllerContext, view, vdd,
                                            new TempDataDictionary(), writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }

        private string GetDetailsTrip(Trip trip)
        {
            var sb = new StringBuilder();
            var a = Controllers.RenderViewToStringAsync();
            sb.Append(@"<html>
                        <head>
                           This is the header of this document.
                       </head>
                        <body>
                            <div class='header'><h1>This is the generated PDF report!!!</h1></div>"
                            
                           @" </body>
                        </ html > ");
            return sb.ToString();
        }
    }
}
