using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryHistoryTrip : RepositoryGeneric<HistoryTrip>, IRepositoryHistoryTrip
    {
        private readonly UsingIdentityContext _context;

        public RepositoryHistoryTrip(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to get list of all HistoryTrip objects with includes from the repository.
        /// </summary>
        /// <returns>Returns list of HistoryTrip objects with includes from the repository.</returns>
        public IQueryable<HistoryTrip> GetAllWithInclude()
        {
            return _context.HistoryTrips
                .Include(p => p.Way)
                .Include(p => p.TimeAfterDeparture);
        }

        /// <summary>
        /// Method to get one HistoryTrip object with includes from the repository.
        /// </summary>
        /// <param name="id">Special unique identifier for HistoryTrip in the repository.</param>
        /// <returns>Returns trip object with includes by id from the repository.</returns>
        public HistoryTrip GetWithIncludeById(int id)
        {
            return GetAllWithInclude().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Method to get list of HistoryTrip objects with includes from the repository.
        /// </summary>
        /// <param name="userId">The list will contain only trips from the user by id</param>
        /// <returns>Returns list of HistoryTrip objects with includes by userId from the repository.</returns>
        public IQueryable<HistoryTrip> GetByUserId(string userId)
        {
            return GetAllWithInclude().Where(p => p.UserId == userId);
        }
    }
}
