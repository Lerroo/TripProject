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

        
    }
}
