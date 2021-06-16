using DinkToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    class WebSettingsDefault : WebSettings
    {
        public WebSettingsDefault()
        {
            base.DefaultEncoding = "utf-8";
            base.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\lib\\bootstrap\\dist\\css\\bootstrap.min.css");
        }
    }
}
