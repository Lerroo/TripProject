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

        private List<SelectListItem> GetSelectListAllYears(string userId, int selectYear)
        {
            var years = new List<SelectListItem>();
            foreach (var vi in _historyTripService.GetHistoryTripYears(userId).Select((x) =>
                                                        new { Value = x.ToString(), Index = x.ToString() }))
            {
                SelectListItem newItem;
                if (vi.Value == selectYear.ToString())
                {
                    newItem = new SelectListItem { Text = vi.Value, Value = vi.Index, Selected = true};
                }
                else
                {
                    newItem = new SelectListItem { Text = vi.Value, Value = vi.Index };
                }
                years.Add(newItem);
            }

            return years;
        }

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

        public UserStatistic GetByYear(int year, string userId)
        {
            List<SelectListItem> years = GetSelectListAllYears(userId, year);
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
