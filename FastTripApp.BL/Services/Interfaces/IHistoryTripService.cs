using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using System.Collections.Generic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IHistoryTripService
    {
        HistoryTrip ConvertToHistoryTrip(Trip trip);
        IEnumerable<int> GetTripYears(string userId);
        CountTrips GetCountTrips(string userId);
        LocationsTrips GetLocationsTrips(string userId);
        ObserveTrips GetDurationTrips(string userId);
        HistoryTrip GetLatsTrip(string userId);
    }
}
