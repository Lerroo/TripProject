using FastTripApp.DAO.Models.Statistic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services.Interfaces
{
    public interface IUserStatisticService
    {
        UserStatistic GetUserStatisticByYear(int year);
    }
}
