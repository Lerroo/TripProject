
using System.Collections.Generic;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryTrip : RepositoryGeneric<Trip>, IRepositoryTrip
    {
        private readonly UsingIdentityContext _сontext;
        public RepositoryTrip(UsingIdentityContext usingIdentityContext):base(usingIdentityContext)
        {
            _сontext = usingIdentityContext;
        }

        public IEnumerable<Trip> TripsByUserId(string id)
        {
            return _сontext.Trips.FromSqlRaw("Select * from Trips where UserId='" + id + "'");
        }
    }
}
