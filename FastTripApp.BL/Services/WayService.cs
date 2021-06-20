using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models.Trip.Way;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.BL.Services
{
    public class WayService : IWayService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryWay _repositoryWay;
        private readonly IRepositoryCoords _repositoryCoords;
        private readonly IRepositoryPlace _repositoryPlace;

        private readonly IHistoryTripService _historyTripService;
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;
        public WayService(
            IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IRepositoryWay repositoryWay,
            IRepositoryCoords repositoryCoords,
            IRepositoryPlace repositoryPlace,

            IHistoryTripService historyTripService,
            IUserService userService,
            IUtilService utilService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = repositoryHistoryTrip;
            _repositoryCoords = repositoryCoords;
            _repositoryWay = repositoryWay;
            _repositoryPlace = repositoryPlace;

            _historyTripService = historyTripService;
            _utilService = utilService;
            _userService = userService;
        }

        public DefaultWay FindWay(DefaultWay findWay)
        {
            DefaultWay foundedWay = _repositoryWay.GetByWay(findWay) ?? new DefaultWay();

            if (findWay.WayId == 0)
            {
                var startPlaceFound = _repositoryPlace.FindByName(findWay.Start.Name);
                if (startPlaceFound != null)
                {
                    foundedWay.Start = startPlaceFound;
                }

                var endPlaceFound = _repositoryPlace.FindByName(findWay.End.Name);
                if (endPlaceFound != null)
                {
                    foundedWay.End = endPlaceFound;
                }
                return foundedWay;
            }          

            return foundedWay;
        }

        public IQueryable<Place> GetNearstPlaces()
        {
            throw new NotImplementedException();
        }
    }
}
