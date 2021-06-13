using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastTripApp.DAO.Models.Enums;

namespace FastTripApp.BL.Services
{
    public class HistoryTripService : IHistoryTripService
    {
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;        

        public HistoryTripService(IRepositoryHistoryTrip repositoryHistoryTrip)
        {
            _repositoryHistoryTrip = repositoryHistoryTrip;            
        }

        /// <summary>
        /// Method to get HistoryTrip object from trip object.
        /// </summary>
        /// <param name="trip">
        /// Object where the information will be taken from.
        /// </param>
        /// <returns>
        /// Return HistoryTrip object from trip data.
        /// </returns>
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

        /// <summary>
        /// Method to get CountTrips object based on list from HistoryTrip repository.
        /// </summary>
        /// <param name="historyTrips">
        /// List from HistoryTrip repository to processing.
        /// </param>
        /// <returns>
        /// Return CountTrips object with statistic info based on list from HistoryTrip repository.
        /// </returns>
        public CountTrips GetCountTrips(IEnumerable<HistoryTrip> historyTrips)
        {            
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

        /// <summary>
        /// Method to get ObserveTrips object based on list from HistoryTrip repository.
        /// </summary>
        /// <param name="historyTrips">
        /// List from HistoryTrip repository to processing.
        /// </param>
        /// <returns>
        /// Return ObserveTrips object with statistic info based on list from HistoryTrip repository. 
        /// </returns>
        public ObserveTrips GetDurationTrips(IEnumerable<HistoryTrip> historyTrips)
        {            
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

        /// <summary>
        /// Method to get LocationsTrips object based on list from HistoryTrip repository.
        /// </summary>
        /// <param name="_historyTrips">
        /// List from HistoryTrip repository to processing.
        /// </param>
        /// <returns>
        /// Return LocationsTrips object with statistic info based on list from HistoryTrip repository.
        /// </returns>
        public LocationsTrips GetLocationsTrips(IEnumerable<HistoryTrip> _historyTrips)
        {
            IEnumerable<HistoryTrip> historyTrips = _historyTrips
                .Where(p=>p.StatusEnum != StatusEnum.Abandon);

            if (historyTrips.FirstOrDefault() == null)
            {
                return new LocationsTrips();
            }

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

        /// <summary>
        /// Method to get list of HistoryTrip based on year and user id.
        /// </summary>
        /// <param name="year">
        /// For this year you need to get statistics.
        /// </param>
        /// <param name="userId">
        /// Statistics for user with userId.
        /// </param>
        /// <returns>
        /// Return list of HistoryTrip based on year and user id.
        /// </returns>
        public IEnumerable<HistoryTrip> GetHistoryByYear(int year, string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetByUserId(userId)
                .Where(p => p.TimeAfterDeparture.End.Value.Year == year);

            return historyTrips;
        }

        /// <summary>
        /// Method to get last Trip from HistoryTrip repository based on year, userId
        /// </summary>
        /// <param name="year">
        /// For this year you need to get statistics.
        /// </param>
        /// <param name="userId">
        /// Statistics for user with userId.
        /// </param>
        /// <returns>
        /// Return HistoryTrip object based on year, userId
        /// </returns>
        public HistoryTrip GetLatsTripByYear(int year, string userId)
        {
            return _repositoryHistoryTrip.GetByUserId(userId)
                .Where(p => p.TimeAfterDeparture.End.Value.Year == year)
                .OrderBy(p => p.TimeAfterDeparture.End)
                .Last();
        }

        /// <summary>
        /// Method to get list years HistoryTrip repository based on userId
        /// </summary>
        /// <param name="userId">
        /// Statistics for user with userId.
        /// </param>
        /// <returns>
        /// Return list years based on userId
        /// </returns>
        public IEnumerable<int> GetHistoryTripYears(string userId)
        {
            IEnumerable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetByUserId(userId);
            IEnumerable<int> years = historyTrips
                .Select(p => p.TimeAfterDeparture.End.Value.Year)
                .Distinct();

            return years;
        }
    }
}
