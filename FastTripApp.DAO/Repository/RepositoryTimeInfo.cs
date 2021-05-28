using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryTimeInfo : RepositoryGeneric<TimeInfo>, IRepositoryTimeInfo
    {
        private readonly UsingIdentityContext _context;
        public RepositoryTimeInfo(UsingIdentityContext usingIdentityContext) : base(usingIdentityContext)
        {
            _context = usingIdentityContext;
        }

        public TimeInfo CalculateTime(TimeInfo timeInfo)
        {
            timeInfo.End = TimeNow();
            timeInfo.TimeTrack = timeInfo.End - timeInfo.Start;
            return timeInfo;
        }

        public DateTime TimeNow()
        {
            return DateTime.UtcNow;
        }
    }
}
