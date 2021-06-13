using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Pdf
{
    public class GeneralTemplate
    {
        public GeneralTemplate()
        {
            Header = "General template header";
            Body = "General template body";
        }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
