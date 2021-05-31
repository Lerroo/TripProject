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
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeAfterDeparture;

        private readonly IHistoryTripService _historyTripService;
        private readonly IUtilService _utilService;
        private readonly ITimeAfterDepartureService _timeAfterDepartureService;

        public TripService(IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IRepositoryTimeAfterDeparture repositoryTimeAfterDeparture,

            IHistoryTripService historyTripService,
            ITimeAfterDepartureService timeAfterDepartureService,
            IUtilService utilService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = repositoryHistoryTrip;
            _repositoryTimeAfterDeparture = repositoryTimeAfterDeparture;

            _historyTripService = historyTripService;
            _utilService = utilService;
            _timeAfterDepartureService = timeAfterDepartureService;
        }

        public Task ToHistory(int? id)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(id);
            var historyTrip = _historyTripService.ConvertToHistoryTrip(trip);            
            _repositoryHistoryTrip.Add(historyTrip);
            _repositoryTrip.Delete(trip.Id);

            return Task.CompletedTask;
        }

        public void Start(int id)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(id);
            trip.TimeAfterDeparture = _repositoryTimeAfterDeparture.GetWithStart();
            _repositoryTrip.Update(trip);
        }

        public void End(int id)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(id);
            _repositoryTimeAfterDeparture.SetEndById(trip.TimeAfterDeparture.Id);
        }
    }
}
