
using System.Collections.Generic;
using System.Linq;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryTrip : RepositoryGeneric<Trip>, IRepositoryTrip
    {
        private readonly UsingIdentityContext _сontext;
        public RepositoryTrip(UsingIdentityContext usingIdentityContext) : base(usingIdentityContext)
        {
            _сontext = usingIdentityContext;
        }

        public Trip GetByIdWithInclude(int? id)
        {
            return _сontext.Trips
                .Include(i => i.Address)
                .Include(i => i.TimeBeforeDeparture)
                .Include(i => i.TimeAfterDeparture)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Trip> TripsByUserId(string id)
        {
            return _сontext.Trips.FromSqlRaw("Select * from Trips where UserId='" + id + "'")
                .Include(p => p.User)
                .Include(r => r.Address)
                .Include(p => p.TimeAfterDeparture)
                .Include(p => p.TimeBeforeDeparture)
                ;
        }
    }
}
