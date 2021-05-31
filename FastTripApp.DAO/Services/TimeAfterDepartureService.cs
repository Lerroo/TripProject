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

        //0001-01-01 00:00:00.0000000
        public DateTime GetErrorTime()
        {
            return new DateTime();
        }




    }
}
