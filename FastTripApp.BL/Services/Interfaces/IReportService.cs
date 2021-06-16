using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Reports;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IReportService
    {

        Task<CustomPdf> GetPdfReportAsync<T>(T model, string viewPathRender);
        Task<string> GetHtmlContentAsync<T>(T model, string viewPathRender);
    }
}
