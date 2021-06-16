using DinkToPdf;
using DinkToPdf.Contracts;

namespace FastTripApp.DAO.Models.Reports
{
    public class CustomPdf
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }        
    }
}
