using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Identity;

namespace FastTripApp.BL.Services
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly IRepositoryUser _repositoryUser;

        private readonly IHistoryTripService _historyTripService;

        public UserStatisticService(
            IRepositoryUser repositoryUser,
            IHistoryTripService historyTripService)
        {
            _repositoryUser = repositoryUser;
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
            foreach (var vi in _historyTripService.GetHistoryTripYears(userId).Select((x) =>
                                                        new { Value = x.ToString(), Index = x.ToString() }))
            {
                var newItem = new SelectListItem { Text = vi.Value, Value = vi.Index };
                years.Add(newItem);
            }

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

            var historyTrip = _historyTripService.GetHistoryByYear(year, userId);
            var statistic = new UserStatistic()
            {
                Year = year,
                Years = years,
                ObserveTrips = _historyTripService.GetDurationTrips(historyTrip),
                CountTrips = _historyTripService.GetCountTrips(historyTrip),
                LocationsTrips = _historyTripService.GetLocationsTrips(historyTrip),
                LastTrip = _historyTripService.GetLatsTripByYear(year, userId),
                User = _repositoryUser.GetById(userId),
            };

            return statistic;
        }
    }
}
