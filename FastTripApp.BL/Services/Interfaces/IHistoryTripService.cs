using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using System.Collections.Generic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IHistoryTripService
    {
        HistoryTrip ConvertToHistoryTrip(Trip trip);
        IEnumerable<int> GetHistoryTripYears(string userId);
        CountTrips GetCountTrips(IEnumerable<HistoryTrip> historyTrips);
        LocationsTrips GetLocationsTrips(IEnumerable<HistoryTrip> historyTrips);
        ObserveTrips GetDurationTrips(IEnumerable<HistoryTrip> historyTrips);
        HistoryTrip GetLatsTripByYear(int year, string userId);
        IEnumerable<HistoryTrip> GetHistoryByYear(int year, string userId);
    }
}
