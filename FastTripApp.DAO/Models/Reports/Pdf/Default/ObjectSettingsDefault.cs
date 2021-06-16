using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    public class ObjectSettingsDefault : ObjectSettings
    {
        public ObjectSettingsDefault()
        {
            base.PagesCount = true;
            base.HtmlContent = "<h1>Default CustomPdf content</h1>";
            base.HeaderSettings = new HeaderSettingsDefault();
            base.FooterSettings = new FooterSettingsDefault();
            base.WebSettings = new WebSettingsDefault();

        }
    }
}
