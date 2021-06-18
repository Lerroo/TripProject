using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    public class GlobalSettingsDefault : GlobalSettings
    {
        public GlobalSettingsDefault()
        {
            base.ColorMode = DinkToPdf.ColorMode.Color;
            base.Orientation = DinkToPdf.Orientation.Portrait;
            base.PaperSize = PaperKind.A4;
            base.Margins = new MarginSettings { Top = 25, Bottom = 25, Right=0, Left=0 };
        }
    }
}
