using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryTimeInfo : IRepository<TimeInfo>
    {
        DateTime TimeNow();
        TimeInfo CalculateTime(TimeInfo timeInfo);
    }
}
