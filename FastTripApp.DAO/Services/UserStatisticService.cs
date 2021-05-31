using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly IRepositoryHistoryTrip _repositoryHistory;
        private readonly IRepositoryUser _repositoryUser;

        private readonly IHistoryTripService _historyTripService;

        public UserStatisticService(
            IRepositoryHistoryTrip repositoryHistory,
            IRepositoryUser repositoryUser,
            IHistoryTripService historyTripService)
        {
            _repositoryHistory = repositoryHistory;
            _repositoryUser = repositoryUser;
            _historyTripService = historyTripService;
        }

        public object GetByUserId(string userId)
        {
            var years = new List<SelectListItem>();
            foreach (var vi in _historyTripService.GetTripYears(userId).Select((x, i) =>
                                                                        new { Value = x.ToString(), Index = i.ToString() }))
            {
                var newItem = new SelectListItem { Text = vi.Value, Value = vi.Index };
                years.Add(newItem);
            }

            var statistic = new UserStatistic()
            {
                Years = years,
                ObserveTrips = _historyTripService.GetDurationTrips(userId),
                CountTrips = _historyTripService.GetCountTrips(userId),
                LocationsTrips = _historyTripService.GetLocationsTrips(userId),
                LastTrip = _repositoryHistory.GetLatsTrip(userId),
                User = _repositoryUser.GetUserById(userId),
            };
            return statistic;
        }
    }
}
