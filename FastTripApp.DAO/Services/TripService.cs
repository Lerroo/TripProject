using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class TripService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        public TripService()
        {
        }

        public TripService(IRepositoryTrip tripRepository, IRepositoryHistoryTrip historyRepository)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;
        }

        public void ToHistory(int id)
        {
            var trip = _repositoryTrip.GetById(id);
            _repositoryHistoryTrip.TripToHistory(trip);
            _repositoryTrip.Delete(trip.Id);
        }
    }
}
