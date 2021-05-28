using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryHistoryTrip : IRepository<HistoryTrip>
    {
        void TripToHistory(Trip trip);
    }
}
