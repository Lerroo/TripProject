using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    class HeaderSettingsDefault : HeaderSettings
    {
        public HeaderSettingsDefault()
        {
            base.FontSize = 15;
            base.FontName = "Ariel";
            base.Right = "Page [page] of [toPage]";
            base.Line = true;
            
        }
    }
}
