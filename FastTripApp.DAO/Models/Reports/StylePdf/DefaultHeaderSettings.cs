using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.StylePdf
{
    class DefaultHeaderSettings : HeaderSettings
    {
        public DefaultHeaderSettings()
        {
            base.FontSize = 15;
            base.FontName = "Ariel";
            base.Right = "Page [page] of [toPage]";
            base.Line = true;
        }
    }
}
