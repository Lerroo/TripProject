using System;

namespace FastTripApp.DAO.Models.Statistic
{
    public class ObserveTrips
    {
        public TimeSpan? Minimum { get; set; }
        public TimeSpan? Maximum { get; set; }
        public TimeSpan? Average { get; set; }
    }
}
