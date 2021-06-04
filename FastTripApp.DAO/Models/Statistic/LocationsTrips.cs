namespace FastTripApp.DAO.Models.Statistic
{

    public class LocationsTrips
    {
        /// <summary>
        /// All proporties contain "Try to book and you will find out"
        /// </summary>
        public LocationsTrips()
        {
            StartFavorite = "Try to book and you will find out";
            EndFavorite = "Try to book and you will find out";
        }
        public string StartFavorite { get; set; }
        public string EndFavorite { get; set; }
    }
}
