using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.StylePdf
{
    public class DefaultObjectSettings : ObjectSettings
    {
        public DefaultObjectSettings()
        {
            base.PagesCount = true;
            base.HtmlContent = "<h1>Default CustomPdf content</h1>";
            base.HeaderSettings = new DefaultHeaderSettings();
            base.FooterSettings = new DefaultFooterSettings();
            base.WebSettings = new DefaultWebSettings();
        }
    }
}
