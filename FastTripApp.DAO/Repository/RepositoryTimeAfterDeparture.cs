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
        private readonly IUtilService _util;
        public RepositoryTimeAfterDeparture(
            UsingIdentityContext usingIdentityContext,
            ITimeAfterDepartureService timeAfterDepartureService,
            IUtilService util) : base(usingIdentityContext)
        {
            _context = usingIdentityContext;
            _timeAfterDepartureService = timeAfterDepartureService;
            _util = util;
        }

        public TimeAfterDeparture getAbandonTime()
        {
            return _context.TimeAfterDepartures.Where(a => 
                a.Start == GetErrorTime()).First();
        }

        /// <summary>
        /// Return bad DateTime like 0001-01-01 00:00:00.0000000
        /// </summary>
        /// <returns></returns>
        public DateTime GetErrorTime()
        {
            return new DateTime();
        }

        /// <summary>
        /// Create new TimeAfterDeparture() with start time = now
        /// </summary>
        /// <returns></returns>
        public TimeAfterDeparture GetWithStart()
        {
            var timeAfterDeparture = new TimeAfterDeparture() { Start = _util.DateTimeNow() };

            _context.Add(timeAfterDeparture);
            return timeAfterDeparture;
        }

        public void SetEndById(int id)
        {
            var timeAfterDeparture = GetById(id);
            timeAfterDeparture.End = _util.DateTimeNow();

            _context.Update(timeAfterDeparture);
        }
    }
}
