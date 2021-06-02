using FastTripApp.DAO.Models.Statistic;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUserStatisticService
    {
        UserStatistic GetByUserId(string userId);
    }
}
