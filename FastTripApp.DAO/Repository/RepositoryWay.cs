using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Trip.Way;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository
{   
    public class RepositoryWay : RepositoryGeneric<DefaultWay>, IRepositoryWay
    {
        private readonly UsingIdentityContext _context;

        public RepositoryWay(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }

        

        /// <summary>
        /// Method to get list of all Trip objects with includes from the repository.
        /// </summary>
        /// <returns>
        /// Returns list of Trip objects with includes from the repository.
        /// </returns>
        public IQueryable<DefaultWay> GetAllWithInclude()
        {
            return _context.Ways
                .Include(p => p.End)
                .ThenInclude(p => p.Coords)
                .Include(p => p.Start)
                .ThenInclude(p => p.Coords);
        }

        /// <summary>
        /// Find way in repository by findWay.Start.Name and findWay.End.Name 
        /// </summary>
        /// <param name="findWay">
        /// The way will be searched by data from this object
        /// </param>
        /// <returns>
        /// Return way found or null
        /// </returns>
        public DefaultWay GetByWay(DefaultWay findWay)
        {
            return GetAllWithInclude()
                .FirstOrDefault(p => p.Start.Name == findWay.Start.Name &&
                    p.End.Name == findWay.End.Name);
        }
    }
}
