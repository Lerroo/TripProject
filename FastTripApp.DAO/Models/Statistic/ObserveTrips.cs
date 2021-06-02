using System;

namespace FastTripApp.DAO.Models.Statistic
{
    public class ObserveTrips
    {
        public ObserveTrips()
        {
            Minimum = new TimeSpan();
            Maximum = new TimeSpan();
            Average = new TimeSpan();
        }
        public TimeSpan? Minimum { get; set; }
        public TimeSpan? Maximum { get; set; }
        public TimeSpan? Average { get; set; }
    }
}
