using FastTripApp.DAO.Models;
using FastTripApp.DAO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class TimeAfterDepartureService : ITimeAfterDepartureService
    {
        private readonly IUtilService _util;

        public TimeAfterDepartureService(IUtilService util)
        {
            _util = util;
        }

        public TimeAfterDeparture CalculateTime(TimeAfterDeparture timeInfo)
        {
            timeInfo.End = _util.DateTimeNow();
            return timeInfo;
        }

       




    }
}
