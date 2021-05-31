using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services.Interfaces
{
    public interface IHistoryTripService
    {
        HistoryTrip ConvertToHistoryTrip(Trip trip);
        void SetStatus(int id);
        IEnumerable<int> GetTripYears(string userId);
        CountTrips GetCountTrips(string userId);
        LocationsTrips GetLocationsTrips(string userId);
        ObserveTrips GetDurationTrips(string userId);
    }
}
