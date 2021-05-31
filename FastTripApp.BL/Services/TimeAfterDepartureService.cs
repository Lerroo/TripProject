using FastTripApp.DAO.Models;
using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Repository.Interfaces;

namespace FastTripApp.BL.Services
{
    public class TimeAfterDepartureService : ITimeAfterDepartureService
    {
        private readonly IUtilService _util;
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeAfterDeparture;

        public TimeAfterDepartureService(
            IRepositoryTimeAfterDeparture repositoryTimeAfterDeparture,

            IUtilService util)
        {
            _repositoryTimeAfterDeparture = repositoryTimeAfterDeparture;

            _util = util;            
        }

        public TimeAfterDeparture CalculateTime(TimeAfterDeparture timeInfo)
        {
            timeInfo.End = _util.DateTimeNow();
            return timeInfo;
        }

        public TimeAfterDeparture GetWithStart()
        {
            var timeAfterDeparture = new TimeAfterDeparture() { Start = _util.DateTimeNow() };

            _repositoryTimeAfterDeparture.Add(timeAfterDeparture);
            return timeAfterDeparture;
        }

        public void SetEndById(int id)
        {
            var timeAfterDeparture = _repositoryTimeAfterDeparture.GetById(id);
            timeAfterDeparture.End = _util.DateTimeNow();

            _repositoryTimeAfterDeparture.Update(timeAfterDeparture);
        }
    }
}
