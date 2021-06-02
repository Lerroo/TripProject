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

        public UserStatistic GetByUserId(string userId)
        {
            var years = new List<SelectListItem>();
            foreach (var vi in _historyTripService.GetTripYears(userId).Select((x, i) =>
                                                        new { Value = x.ToString(), Index = i.ToString() }))
            {
                var newItem = new SelectListItem { Text = vi.Value, Value = vi.Index };
                years.Add(newItem);
            }

            //history clear
            if (years.Count == 0)
            {
                return new UserStatistic() {
                    Years = new List<SelectListItem>(),
                    ObserveTrips = new ObserveTrips(),
                    CountTrips = new CountTrips(),
                    LocationsTrips = new LocationsTrips(),
                    LastTrip = null,
                    User = _repositoryUser.GetById(userId),
                };
            }

            var statistic = new UserStatistic()
            {
                Years = years,
                ObserveTrips = _historyTripService.GetDurationTrips(userId),
                CountTrips = _historyTripService.GetCountTrips(userId),
                LocationsTrips = _historyTripService.GetLocationsTrips(userId),
                LastTrip = _historyTripService.GetLatsTrip(userId),
                User = _repositoryUser.GetById(userId),
            };
            return statistic;
        }
    }
}
