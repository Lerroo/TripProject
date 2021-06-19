using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Models.Trip;
using System.Collections.Generic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IHistoryTripService
    {
        HistoryTrip ConvertToHistoryTrip(DefaultTrip trip);
    }
}
