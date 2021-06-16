using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastTripApp.DAO.Models.Reports
{
    public class MostPopularTrip
    {
        public Way Way { get; set; }
        [DisplayName("Number of orders at the moment")]
        public int Count { get; set; }
    }
}

