
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FastTripApp.DAO.Repository
{
    public class RepositoryHistoryTrip : RepositoryGeneric<HistoryTrip>, IRepositoryHistoryTrip
    {
        private readonly UsingIdentityContext _context;

        public RepositoryHistoryTrip(UsingIdentityContext context) :base(context)
        {
            _context = context;
        }

        public void TripToHistory(Trip trip)
        {
            var timeTrack = trip.TimeInfo.End - trip.TimeInfo.Start;
            HistoryTrip historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                Image = trip.Image,
                Descriprion = trip.Descriprion,
                StartTrip = trip.TimeInfo.Start,
                EndTrip = trip.TimeInfo.End,
                TimeTrack = timeTrack.Value.Seconds,
                AddressStart = trip.AddressStart,
                AddressEnd = trip.AddressEnd,
                AddressEndLatitude = trip.AddressEndLatitude,
                AddressEndLongitude = trip.AddressEndLongitude,
                AddressStartLatitude = trip.AddressStartLatitude,
                AddressStartLongitude = trip.AddressStartLongitude,
                UserId = trip.UserId
            };

            _context.Add(historyTrip);
        }

    }
}
