using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Statistic
{
    public class Trips
    {
        public int CountAll { get; set; }
        public int CountAbandon { get; set; }
        public int CountSuccess { get; set; }
        public HistoryTrip LastTrip { get; set; }
    }
}
