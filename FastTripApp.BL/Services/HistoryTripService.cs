﻿using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Statistic;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastTripApp.DAO.Models.Enums;
using FastTripApp.DAO.Models.Trip;

namespace FastTripApp.BL.Services
{
    public class HistoryTripService : IHistoryTripService
    {
        /// <summary>
        /// Method to get HistoryTrip object from trip object.
        /// </summary>
        /// <param name="trip">
        /// Object where the information will be taken from.
        /// </param>
        /// <returns>
        /// Return HistoryTrip object from trip data.
        /// </returns>
        public HistoryTrip ConvertToHistoryTrip(DefaultTrip trip)
        {
            var historyTrip = new HistoryTrip
            {
                TripId = trip.Id,
                Name = trip.Name,
                Descriprion = trip.Descriprion,
                TimeAfterDeparture = trip.TimeAfterDeparture,
                Way = trip.Way,
                StatusEnum = trip.StatusEnum,
                UserId = trip.UserId
            };
            return historyTrip;
        }
    }
}
