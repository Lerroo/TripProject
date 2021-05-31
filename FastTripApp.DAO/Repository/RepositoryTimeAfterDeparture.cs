using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryTimeAfterDeparture : RepositoryGeneric<TimeAfterDeparture>, IRepositoryTimeAfterDeparture
    {
        private readonly UsingIdentityContext _context;
        private readonly ITimeAfterDepartureService _timeAfterDepartureService;
        public RepositoryTimeAfterDeparture(
            UsingIdentityContext usingIdentityContext,
            ITimeAfterDepartureService timeAfterDepartureService) : base(usingIdentityContext)
        {
            _context = usingIdentityContext;
            _timeAfterDepartureService = timeAfterDepartureService;
        }

        public TimeAfterDeparture getAbandonTime()
        {
            return _context.TimeAfterDepartures.Where(a => 
                a.Start == _timeAfterDepartureService.GetErrorTime()).First();
        }
    }
}
