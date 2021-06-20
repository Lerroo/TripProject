using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Enums;
using System;
using FastTripApp.DAO.Models.Reports;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using FastTripApp.DAO.Models.Trip.Way;
using FastTripApp.DAO.Models.Trip;

namespace FastTripApp.BL.Services
{
    public class TripService : ITripService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryWay _repositoryWay;
        private readonly IRepositoryCoords _repositoryCoords;
        private readonly IRepositoryPlace _repositoryPlace;

        private readonly IHistoryTripService _historyTripService;
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;
        private readonly IWayService _wayService;

        public TripService(
            IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,
            IRepositoryWay repositoryWay,
            IRepositoryCoords repositoryCoords,
            IRepositoryPlace repositoryPlace,

            IHistoryTripService historyTripService,
            IWayService wayService,
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
            _wayService = wayService;
        }

        /// <summary>
        /// Sends a trip to history and remove from booked trips.
        /// </summary>
        /// <param name="idTrip">
        /// Trip id to save it in historyTrip repository.
        /// </param>
        /// <returns>
        /// Status task
        /// </returns>
        public Task ToHistory(int? idTrip)
        {
            var trip = _repositoryTrip.GetWithIncludeById(idTrip);
            var historyTrip = _historyTripService.ConvertToHistoryTrip(trip);
            _repositoryHistoryTrip.Add(historyTrip);

            _repositoryTrip.Delete(trip);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Set start time information in trip repository.
        /// </summary>
        /// <param name="idTrip">
        /// Trip id to update it in Trip repository.
        /// </param>
        public void Start(int? idTrip)
        {
            var trip = _repositoryTrip.GetWithIncludeById(idTrip);
            trip.TimeAfterDeparture = GetTimeAfterDepartureStart(trip);

            _repositoryTrip.Update(trip);
        }

        /// <summary>
        /// Set end time information in trip repository.
        /// </summary>
        /// <param name="idTrip">
        /// Trip id to update it in Trip repository.
        /// </param>
        public void End(int? idTrip)
        {
            var trip = _repositoryTrip.GetWithIncludeById(idTrip);
            trip.TimeAfterDeparture = GetTimeAfterDepartureEnd(trip);
            trip.StatusEnum = GetStatus(trip);
            _repositoryTrip.Update(trip);

            ToHistory(idTrip);
        }

        /// <summary>
        /// Get StatusEnum for trip based on trip.TimeAfterDeparture.Observe.
        /// </summary>
        /// <param name="trip">
        /// Trip object for set status.
        /// </param>
        /// <returns>
        /// Returns trip status based on trip.TimeAfterDeparture.Observe.
        /// </returns>
        private StatusEnum GetStatus(DefaultTrip trip)
        {            
            if (trip.TimeAfterDeparture.Observe.Value.TotalSeconds == 0)
            {
                trip.StatusEnum = StatusEnum.Abandon;
            }
            else
            {
                trip.StatusEnum = StatusEnum.Success;
            }

            return trip.StatusEnum;
        }

        /// <summary>
        /// Getting TimeAfterDeparture with end time trip.
        /// </summary>
        /// <param name="trip">
        /// Trip object for set end time trip.
        /// </param>
        /// <returns>
        /// Return TimeAfterDeparture object contain info about end trip.
        /// </returns>
        private TimeAfterDeparture GetTimeAfterDepartureEnd(DefaultTrip trip)
        {
            //trip is abandon
            if (trip.TimeAfterDeparture == null)
            {
                trip.TimeAfterDeparture = new TimeAfterDeparture();
            }
            else
            {
                trip.TimeAfterDeparture.End = _utilService.GetDateTimeNow();
            }            

            return trip.TimeAfterDeparture;
        }

        /// <summary>
        /// Getting TimeAfterDeparture with start time for trip.
        /// </summary>
        /// <param name="trip">
        /// Trip object for set start time trip.
        /// </param>
        /// <returns>
        /// Return TimeAfterDeparture object contain info about start trip.
        /// </returns>
        public TimeAfterDeparture GetTimeAfterDepartureStart(DefaultTrip trip)
        {
            trip.TimeAfterDeparture = new TimeAfterDeparture()
            {
                Start = _utilService.GetDateTimeNow()
            };                

            return trip.TimeAfterDeparture;
        }

        /// <summary>
        /// A new trip is added to the Trip repository and the route image is loaded asynchronously.
        /// </summary>
        /// <param name="trip">
        /// Add trip object in Trip repository
        /// </param> 
        /// <returns>
        /// </returns>
        public async void AddNewTripAsync(DefaultTrip trip, Uri uri)
        {
            trip.User = _userService.GetCurrentUser();
            
            trip.Way = _wayService.FindWay(trip.Way) ?? trip.Way;          

            if (trip.Way.WayId == 0)
            {
                trip.Way.StaticImage = GenerateImageFileName();
                await _utilService.DownloadUriContentAsync(uri, trip.UserId, trip.Way.StaticImage);
            }            

            _repositoryTrip.Add(trip);
        }       
         
        public FindMostPopularTrip GetMostPopularTrip(string userId)
        {
            var listTrips = _repositoryTrip.GetAllWithIncludeByUserId(userId);
            var orderByCount = listTrips.ToList()
                .GroupBy(p => p.Way)
                .OrderByDescending(p => p.Count())
                .Select(p => new { Way = p.Key, Count = p.Count(),})
                .FirstOrDefault();
            var tripMostPopular = new FindMostPopularTrip
            {
                Way = orderByCount.Way,
                Count = orderByCount.Count
            };
            return tripMostPopular;
        }

        public IEnumerable<NearestPlaces> GetNearstPlaces(Coords centerCoords, double radiusDistance)
        {
            var allPlace = _repositoryPlace.GetAllWithInclude();
            var nearstPlaces = new List<NearestPlaces>();
            foreach (var place in allPlace)
            {
                var distance = Distance(centerCoords, place.Coords);
                if (distance < radiusDistance)
                {
                    NearestPlaces nearestPlaces = new NearestPlaces()
                    {
                        Place = place,
                        Distance = distance
                    };
                    nearstPlaces.Add(nearestPlaces);
                }
            }

            return nearstPlaces;
        }

        public double Distance(Coords center, Coords pos2)
        {
            const double R = 6378.1; //radius of earth in km

            double dLat = ToRadian(pos2.Lat - center.Lat);
            double dLng = ToRadian(pos2.Lng - center.Lng);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + 
                Math.Cos(ToRadian(center.Lat)) * Math.Cos(ToRadian(pos2.Lat)) * Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));

            double d = R * c;
            return d;
        }

        /// <summary>
        /// Convert to Radians.
        /// </summary>
        private double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

        /// <summary>
        /// Trip update and replacement of the old photo of the route
        /// </summary>
        /// <param name="trip">
        /// Update trip object in Trip repository
        /// </param>
        /// <returns></returns>
        public async void UpdateTripAsync(DefaultTrip trip, Uri uri)
        {
            trip.Way.StaticImage = $@"{Guid.NewGuid()}.png";
            _repositoryTrip.Update(trip);

            await _utilService.DownloadUriContentAsync(uri, trip.UserId, trip.Way.StaticImage);
        }

        private string GenerateImageFileName()
        {
            return $@"{_utilService.GetGuid()}.png";
        }

        public IEnumerable<DefaultTrip> GetNearstTrip()
        {
            throw new NotImplementedException();
        }
    }
}

