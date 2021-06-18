using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Models.Enums;
using System;

namespace FastTripApp.BL.Services
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        private readonly IHistoryTripService _historyTripService;

        public UserStatisticService(
            IRepositoryUser repositoryUser,
            IRepositoryHistoryTrip repositoryHistoryTrip,

            IHistoryTripService historyTripService)
        {
            _repositoryUser = repositoryUser;
            _repositoryHistoryTrip = repositoryHistoryTrip;

            _historyTripService = historyTripService;
        }

        /// <summary>
        /// Method to get list of all years for <select> from HistoryTrip repository.
        /// </summary>
        /// <param name="userId">
        /// The list will contain only trips from the user by id.
        /// </param>
        /// <returns>
        /// Returns list of years for <select> by userId from HistoryTrip repository.
        /// </returns>
        private List<SelectListItem> GetAllYears(string userId)
        {
            var years = new List<SelectListItem>();
            foreach (var vi in GetHistoryTripYears(userId).Select((x) =>
                                                        new { Value = x.ToString(), Index = x.ToString() }))
            {
                var newItem = new SelectListItem { Text = vi.Value, Value = vi.Index };
                years.Add(newItem);
            }

            return years;
        }

        /// <summary>
        /// Method for get one UserStatistic object for a specific year for a specific user based on data from HistoryTrip retository.
        /// </summary>
        /// <param name="year">For this year you need to get statistics.</param>
        /// <param name="userId">Statistics for user with userId.</param>
        /// <returns>Return UserStatistic object based on data from HistoryTrip retository.</returns>
        public UserStatistic GetByYear(int year, string userId)
        {
            List<SelectListItem> years = GetAllYears(userId);
            //history clear
            if (years.Count == 0)
            {
                return GetDefaultUserStatistic(userId);
            }

            var historyTrip = GetHistoryByYear(year, userId);
            var statistic = new UserStatistic()
            {
                Year = year,
                Years = years,
                ObserveTrips = GetDurationTrips(historyTrip),
                CountTrips = GetCountTrips(historyTrip),
                LocationsTrips = GetLocationsTrips(historyTrip),
                LastTrip = GetLatsTripByYear(year, userId),
                User = _repositoryUser.GetById(userId),
            };

            return statistic;
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
        public CountTrips GetCountTrips(IQueryable<HistoryTrip> historyTrips)
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
        public ObserveTrips GetDurationTrips(IQueryable<HistoryTrip> historyTrips)
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
        public LocationsTrips GetLocationsTrips(IQueryable<HistoryTrip> _historyTrips)
        {
            IEnumerable<HistoryTrip> historyTrips = _historyTrips
                .Where(p => p.StatusEnum != StatusEnum.Abandon);

            if (historyTrips.FirstOrDefault() == null)
            {
                return new LocationsTrips();
            }

            string StartFavoritePlace = historyTrips.GroupBy(id => id.Way.Start.Name)
                .OrderByDescending(id => id.Count())
                .Select(p => p.Key)
                .First();
            string EndFavoritePlace = historyTrips.GroupBy(id => id.Way.End.Name)
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
        public IQueryable<HistoryTrip> GetHistoryByYear(int year, string userId)
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
        public IQueryable<int> GetHistoryTripYears(string userId)
        {
            IQueryable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetByUserId(userId);
            IQueryable<int> years = historyTrips
                .Select(p => p.TimeAfterDeparture.End.Value.Year)
                .Distinct();

            return years;
        }

        /// <summary>
        /// Method to get one UserStatistic object with default properties. 
        /// LastTrip = null
        /// </summary>
        /// <param name="userId">User property set by user id.</param>
        /// <returns>Returns UserStatistic object with user by id.</returns>
        private UserStatistic GetDefaultUserStatistic(string userId)
        {
            return new UserStatistic()
            {
                Years = new List<SelectListItem>(),
                ObserveTrips = new ObserveTrips(),
                CountTrips = new CountTrips(),
                LocationsTrips = new LocationsTrips(),
                LastTrip = null,
                User = _repositoryUser.GetById(userId),
            };
        }
    }
}
