using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IReportService
    {
        public CustomPdf GeneratePdfReport(Trip trip);
    }
}
