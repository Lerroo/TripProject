using FastTripApp.DAO.Models;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface ITimeAfterDepartureService
    {
        TimeAfterDeparture CalculateTime(TimeAfterDeparture timeInfo);
        TimeAfterDeparture GetWithStart();
        void SetEndById(int id);
    }
}
