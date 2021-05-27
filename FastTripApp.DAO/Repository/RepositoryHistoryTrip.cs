
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FastTripApp.DAO.Repository
{
    public class RepositoryHistoryTrip : RepositoryGeneric<HistoryTrip>, IRepositoryHistory
    {
        private readonly UsingIdentityContext _context;

        public RepositoryHistoryTrip(UsingIdentityContext context) :base(context)
        {
            _context = context;
        }

        public void TripToHistory(Trip trip)
        {
            HistoryTrip historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                TimePlain = trip.TimePlain,
                EstimatedTime = trip.EstimatedTime,
                Image = trip.Image,
                Descriprion = trip.Descriprion,
                StartTrip = trip.TimeInfo.Start,
                EndTrip = trip.TimeInfo.End,
                TimeTrack = trip.TimeInfo.TimeTrack,
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
