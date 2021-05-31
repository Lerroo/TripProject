

using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Linq;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryTimeAfterDeparture : RepositoryGeneric<TimeAfterDeparture>, IRepositoryTimeAfterDeparture
    {
        private readonly UsingIdentityContext _context;

        public RepositoryTimeAfterDeparture(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }

        public TimeAfterDeparture getAbandonTime()
        {

            return _context.TimeAfterDepartures.Where(a => 
                a.Start == new DateTime()).First();
        }        
    }
}
