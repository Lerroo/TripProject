
using System.Collections.Generic;
using System.Linq;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.StatusEnum;
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

        public void SetStatus(int id)
        {
            var trip = GetByIdWithInclude(id);
            if (trip.TimeAfterDeparture.Observe.Value.TotalSeconds == 0)
            {
                trip.StatusEnum = Status.Abandon;
            }
            else
            {
                trip.StatusEnum = Status.Success;
            }
            _сontext.Update(trip);
        }

        public IEnumerable<Trip> TripsByUserId(string id)
        {
            return _сontext.Trips
                .Include(p => p.User)
                .Include(r => r.Address)
                .Include(p => p.TimeAfterDeparture)
                .Include(p => p.TimeBeforeDeparture)
                .Where(p =>p.UserId == id);
        }


    }
}
