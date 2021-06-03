using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.StatusEnum;
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
        /// Sends a trip to history and remove from booked trips
        /// </summary>
        /// <param name="idTrip"></param>
        /// <returns></returns>
        public Task ToHistory(int? idTrip)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(idTrip);
            var historyTrip = _historyTripService.ConvertToHistoryTrip(trip);            
            _repositoryHistoryTrip.Add(historyTrip);

            _repositoryTrip.Delete(trip.Id);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Set time information about the start of the trip
        /// </summary>
        /// <param name="idTrip"></param>
        public void Start(int? idTrip)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(idTrip);
            trip.TimeAfterDeparture = GetTimeAfterDepartureStart(trip);

            _repositoryTrip.Update(trip);
        }

        /// <summary>
        /// Set time information about the end of the trip
        /// </summary>
        /// <param name="idTrip"></param>
        public void End(int? idTrip)
        {
            var trip = _repositoryTrip.GetByIdWithInclude(idTrip);
            trip.TimeAfterDeparture = GetTimeAfterDepartureEnd(trip);
            trip.StatusEnum = GetStatus(trip);
            _repositoryTrip.Update(trip);

            ToHistory(idTrip);
        }

        /// <summary>
        /// Get StatusEnum for trip based on trip.TimeAfterDeparture.Observe
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
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
        /// Getting TimeAfterDeparture with end time for trip
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        private TimeAfterDeparture GetTimeAfterDepartureEnd(Trip trip)
        {
            //trip is abandon
            if (trip.TimeAfterDeparture == null)
            {
                trip.TimeAfterDeparture = new TimeAfterDeparture();
            }
            else
            {
                trip.TimeAfterDeparture.End = _utilService.DateTimeNow();
            }            

            return trip.TimeAfterDeparture;
        }

        /// <summary>
        /// Getting TimeAfterDeparture with start time for trip
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        public TimeAfterDeparture GetTimeAfterDepartureStart(Trip trip)
        {
            trip.TimeAfterDeparture = new TimeAfterDeparture()
            {
                Start = _utilService.DateTimeNow()
            };                

            return trip.TimeAfterDeparture;
        }

        public async void AddNewTrip(Trip trip)
        {
            trip.UserId = _userService.GetCurrentUserId();
            trip.StaticImageWay = $@"{Guid.NewGuid()}.png";
            _repositoryTrip.Add(trip);

            await GetStaticImageWay(trip);            
        }

        private async Task<Task> GetStaticImageWay(Trip trip) 
        { 
            await _utilService.DownloadAsync(new Uri(trip.StaticImageWayUrl), trip.UserId, trip.StaticImageWay);
            return Task.CompletedTask;
        }

        public async void UpdateTrip(Trip trip)
        {
            trip.StaticImageWay = $@"{Guid.NewGuid()}.png";
            _repositoryTrip.Update(trip);

            await GetStaticImageWay(trip);            
        }
    }
}

