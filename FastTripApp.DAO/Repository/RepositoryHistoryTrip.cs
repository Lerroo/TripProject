
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FastTripApp.DAO.Repository
{
    public class RepositoryHistoryTrip : RepositoryGeneric<HistoryTrip>, IRepositoryHistoryTrip
    {
        private readonly UsingIdentityContext _context;

        public RepositoryHistoryTrip(UsingIdentityContext context) :base(context)
        {
            _context = context;
        }

        public HistoryTrip GetWithInclude(int id)
        {
            return _context.HistoryTrips
                .Include(p => p.Address)
                .Include(p => p.TimeAfterDeparture)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<HistoryTrip> HistoryByUserId(string id)
        {
            return _context.HistoryTrips
                .Include(p => p.Address)
                .Include(p => p.TimeAfterDeparture)
                .Where(p => p.UserId == id);
        }

    }
}
