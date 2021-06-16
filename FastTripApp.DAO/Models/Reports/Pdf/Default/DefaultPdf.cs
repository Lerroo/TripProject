using DinkToPdf;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    public class DefaultPdf : HtmlToPdfDocument
    {
        public DefaultPdf(string htmlContent)
        {
            base.GlobalSettings = new GlobalSettingsDefault();
            var htmlContent1 = htmlContent.Replace("\"", "\'");
            base.Objects.Add(new ObjectSettingsDefault() { HtmlContent = htmlContent1 });
        }
    }
}

