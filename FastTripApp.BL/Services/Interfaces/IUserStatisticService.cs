using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using System.Collections.Generic;
using System.Linq;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUserStatisticService
    {
        UserStatistic GetByYear(int year, string userId);
        IQueryable<int> GetHistoryTripYears(string userId);
        CountTrips GetCountTrips(IQueryable<HistoryTrip> historyTrips);
        LocationsTrips GetLocationsTrips(IQueryable<HistoryTrip> historyTrips);
        ObserveTrips GetDurationTrips(IQueryable<HistoryTrip> historyTrips);
        HistoryTrip GetLatsTripByYear(int year, string userId);
        IQueryable<HistoryTrip> GetHistoryByYear(int year, string userId);
    }
}
