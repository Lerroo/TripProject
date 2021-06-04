using FastTripApp.DAO.Models.Statistic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUserStatisticService
    {
        UserStatistic GetByYear(int year, string userId);
    }
}
