
using System.Collections.Generic;
using System.Linq;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Enums;
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

        /// <summary>
        /// Method to get list of all Trip objects with includes from the repository.
        /// </summary>
        /// <returns>
        /// Returns list of Trip objects with includes from the repository.
        /// </returns>
        public IQueryable<Trip> GetAllWithInclude()
        {
            return _сontext.Trips
               .Include(p => p.User)
               .Include(i => i.Address)
               .Include(i => i.TimeBeforeDeparture)
               .Include(i => i.TimeAfterDeparture);
        }

        /// <summary>
        /// Method to get one Trip object with includes from the repository.
        /// </summary>
        /// <param name="id">
        /// Special unique identifier for trip in the repository.
        /// </param>
        /// <returns>
        /// Returns Trip object with includes by id from the repository.
        /// </returns>
        public Trip GetWithIncludeById(int? id)
        {
            return GetAllWithInclude().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method to get list of Trip objects with includes from the repository.
        /// </summary>
        /// <param name="userId">
        /// The list will contain only trips from the user by id.
        /// </param>
        /// <returns>
        /// Returns list of Trip objects with includes by userId from the repository.
        /// </returns>
        public IQueryable<Trip> GetWithIncludeByUserId(string userId)
        {
            return GetAllWithInclude().Where(p => p.UserId == userId);
        }
    }
}
