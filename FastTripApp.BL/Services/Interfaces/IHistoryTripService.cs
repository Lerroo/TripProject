using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using System.Collections.Generic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IHistoryTripService
    {
        HistoryTrip ConvertToHistoryTrip(Trip trip);
    }
}
