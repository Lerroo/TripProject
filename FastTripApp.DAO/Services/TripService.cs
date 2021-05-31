using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Services
{
    public class TripService : ITripService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IHistoryTripService _historyTripService;

        public TripService(IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IHistoryTripService historyTripService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = repositoryHistoryTrip;
            _historyTripService = historyTripService;
        }

        public Task ToHistory(int? id)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(id);
            var historyTrip = _historyTripService.ConvertToHistoryTrip(trip);            
            _repositoryHistoryTrip.Add(historyTrip);
            _repositoryTrip.Delete(trip.Id);

            return Task.CompletedTask;
        }
    }
}
