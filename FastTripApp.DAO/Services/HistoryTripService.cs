using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class HistoryTripService : IHistoryTripService
    {
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeAfterDeparture;

        public HistoryTripService(IRepositoryTimeAfterDeparture repositoryTimeAfterDeparture)
        {
            _repositoryTimeAfterDeparture = repositoryTimeAfterDeparture;
        }

        public HistoryTrip ConvertToHistoryTrip(Trip trip)
        {
            var historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                Image = trip.Image,
                Descriprion = trip.Descriprion,
                TimeAfterDeparture = trip.TimeAfterDeparture ??= _repositoryTimeAfterDeparture.getAbandonTime(),
                Address = trip.Address,
                StatusEnum = trip.StatusEnum,
                UserId = trip.UserId
            };
            return historyTrip;
        }

        public void SetStatus(int id)
        {
            throw new NotImplementedException();
        }
    }
}
