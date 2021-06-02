using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastTripApp.DAO.Models.StatusEnum;

namespace FastTripApp.BL.Services
{
    public class HistoryTripService : IHistoryTripService
    {
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeAfterDeparture;

        public HistoryTripService(
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IRepositoryTimeAfterDeparture repositoryTimeAfterDeparture)
        {
            _repositoryHistoryTrip = repositoryHistoryTrip;
            _repositoryTimeAfterDeparture = repositoryTimeAfterDeparture;
        }

        public HistoryTrip ConvertToHistoryTrip(Trip trip)
        {
            var historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                StaticImageWay = trip.StaticImageWay,
                Descriprion = trip.Descriprion,
                TimeAfterDeparture = trip.TimeAfterDeparture,
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
            int CountAbandon = historyTrips.Where(p => p.StatusEnum == StatusEnum.Abandon).Count();
            int CountSucces = historyTrips.Where(p => p.StatusEnum == StatusEnum.Success).Count();

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

        public HistoryTrip GetLatsTrip(string userId)
        {
            return _repositoryHistoryTrip.GetHistoryByUserId(userId)
                .OrderBy(p=>p.TimeAfterDeparture.End)
                .Last();
        }

        public LocationsTrips GetLocationsTrips(string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId)
                .Where(p=>p.StatusEnum != StatusEnum.Abandon);

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
            IEnumerable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetHistoryByUserId(userId);
            var la = historyTrips.ToList().Count;
            // .Where(a=>a != 1) if Year error => new DateTime().Year
            IEnumerable<int> years = historyTrips
                .Select(p => p.TimeAfterDeparture.End.Value.Year)
                .Distinct();
            return years;
        }


    }
}
