using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Models.Statistic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services.Interfaces
{
    public interface IUserStatisticService
    {
        object GetByUserId(string userId);
    }
}
