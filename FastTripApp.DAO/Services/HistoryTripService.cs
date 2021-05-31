using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class HistoryTripService : IHistoryTripService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeAfterDeparture;

        private readonly IUtilService _utilService;
        private readonly ITimeAfterDepartureService _timeAfterDepartureService;

        public HistoryTripService(IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IRepositoryTimeAfterDeparture repositoryTimeAfterDeparture,
            ITimeAfterDepartureService timeAfterDepartureService,
            IUtilService utilService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = repositoryHistoryTrip;
            _repositoryTimeAfterDeparture = repositoryTimeAfterDeparture;

            _utilService = utilService;
            _timeAfterDepartureService = timeAfterDepartureService;
        }

        public HistoryTrip ConvertToHistoryTrip(Trip trip)
        {
            var historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                Image = trip.Image,
                Descriprion = trip.Descriprion,
                TimeAfterDeparture = trip.TimeAfterDeparture ??= _repositoryTimeAfterDeparture.getAbandonTime(),
                Address = trip.Address,
                StatusEnum = trip.StatusEnum,
                UserId = trip.UserId
            };
            return historyTrip;
        }

        public CountTrips GetCountTrips(string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId);
            
            int CountAll = historyTrips.Count();
            int CountAbandon = historyTrips.Where(p => p.StatusEnum == Models.StatusEnum.Status.Abandon).Count();
            int CountSucces = historyTrips.Where(p => p.StatusEnum == Models.StatusEnum.Status.Success).Count();

            var newCountTrips = new CountTrips()
            {
                All = CountAll,
                Abandon = CountAbandon,
                Success = CountSucces
            };
            return newCountTrips;
        }

        public ObserveTrips GetDurationTrips(string userId)
        {
            IEnumerable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId);
            
            var minimumSec = historyTrips.Min(p => p.TimeAfterDeparture.Observe.Value.TotalSeconds);
            TimeSpan? minimum = TimeSpan.FromSeconds(Convert.ToInt32(minimumSec));

            var maximumSec = historyTrips.Max(p => p.TimeAfterDeparture.Observe.Value.TotalSeconds);
            TimeSpan? maximum = TimeSpan.FromSeconds(Convert.ToInt32(maximumSec));

            var averageSecond = historyTrips.Select(p => p.TimeAfterDeparture.Observe.Value.TotalSeconds).Average();
            TimeSpan? average = TimeSpan.FromSeconds(Convert.ToInt32(averageSecond));

            var newDuration = new ObserveTrips()
            {
                Minimum = minimum,
                Maximum = maximum,
                Average = average
            };
            return newDuration;            
        }

        public LocationsTrips GetLocationsTrips(string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId);

            string StartFavoritePlace = historyTrips.GroupBy(id => id.Address.Start)
                .OrderByDescending(id => id.Count())
                .Select(p => p.Key)
                .First();
            string EndFavoritePlace = historyTrips.GroupBy(id => id.Address.End)
                .OrderByDescending(id => id.Count())
                .Select(p => p.Key)
                .First();
            var newLocation = new LocationsTrips()
            {
                StartFavorite = StartFavoritePlace,
                EndFavorite = EndFavoritePlace
            };

            return newLocation;
        }

        public IEnumerable<int> GetTripYears(string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId);
            
            // .Where(a=>a != 1) if Year error => new DateTime().Year
            IEnumerable<int> years = historyTrips
                .Select(p => p.TimeAfterDeparture.End.Value.Year)
                .Where(a=>a != 1) 
                .Distinct();
            return years;
        }

        public void SetStatus(int id)
        {
            throw new NotImplementedException();
        }
    }
}
