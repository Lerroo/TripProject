using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.StylePdf
{
    class DefaultWebSettings : WebSettings
    {
        public DefaultWebSettings()
        {
            base.DefaultEncoding = "utf-8";
        }
    }
}
