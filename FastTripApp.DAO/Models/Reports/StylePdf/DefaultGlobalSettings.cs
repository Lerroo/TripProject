using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.StylePdf
{
    public class DefaultGlobalSettings : GlobalSettings
    {
        public DefaultGlobalSettings()
        {
            base.ColorMode = DinkToPdf.ColorMode.Grayscale;
            base.Orientation = DinkToPdf.Orientation.Portrait;
            base.PaperSize = PaperKind.A4;
            base.Margins = new MarginSettings { Top = 25, Bottom = 25 };
        }
    }
}
