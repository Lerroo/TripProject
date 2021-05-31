using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services.Interfaces
{
    public interface ITimeAfterDepartureService
    {
        TimeAfterDeparture CalculateTime(TimeAfterDeparture timeInfo);
        DateTime GetErrorTime();
    }
}
