using System;

namespace FastTripApp.DAO.Models.Statistic
{
    public class ObserveTrips
    {
        /// <summary>
        /// All properties =  new TimeSpan()
        /// </summary>
        public ObserveTrips()
        {
            var timeSpan000 = new TimeSpan();
            Minimum = timeSpan000;
            Maximum = timeSpan000;
            Average = timeSpan000;
        }
        public TimeSpan? Minimum { get; set; }
        public TimeSpan? Maximum { get; set; }
        public TimeSpan? Average { get; set; }
    }
}
