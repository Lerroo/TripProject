using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Enums;
using System;

namespace FastTripApp.BL.Services
{
    public class TripService : ITripService
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;        

        private readonly IHistoryTripService _historyTripService;
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;        

        public TripService(
            IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip repositoryHistoryTrip,            

            IHistoryTripService historyTripService,
            IUserService userService,
            IUtilService utilService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = repositoryHistoryTrip;            

            _historyTripService = historyTripService;
            _utilService = utilService;
            _userService= userService;            
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
        private StatusEnum GetStatus(Trip trip)
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
        private TimeAfterDeparture GetTimeAfterDepartureEnd(Trip trip)
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
        public TimeAfterDeparture GetTimeAfterDepartureStart(Trip trip)
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
        public async Task AddNewTripAsync(Trip trip)
        {
            trip.UserId = _userService.GetCurrentUserId();
            trip.StaticImageWay = GenerateImageFileName();
            await DownloadStaticImageWayAsync(trip);

            _repositoryTrip.Add(trip);
        }

        private string GenerateImageFileName()
        {
            return $@"{_utilService.GetGuid()}.png";
        }

        /// <summary>
        /// Road photo is loaded asynchronously for the trip
        /// </summary>
        /// <param name="trip">
        /// information to download the image is taken from trip object
        /// </param>
        /// <returns></returns>
        private async Task DownloadStaticImageWayAsync(Trip trip) 
        {
            var uri = new Uri(trip.StaticImageWayUrl);
            await _utilService.DownloadUriContentAsync(uri, trip.UserId, trip.StaticImageWay);
        }

        /// <summary>
        /// Trip update and replacement of the old photo of the route
        /// </summary>
        /// <param name="trip">
        /// Update trip object in Trip repository
        /// </param>
        /// <returns></returns>
        public async Task UpdateTripAsync(Trip trip)
        {
            trip.StaticImageWay = $@"{Guid.NewGuid()}.png";
            _repositoryTrip.Update(trip);

            await DownloadStaticImageWayAsync(trip);            
        }
    }
}

