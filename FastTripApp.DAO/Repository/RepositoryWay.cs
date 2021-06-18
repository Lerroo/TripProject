using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository
{   
    public class RepositoryWay : RepositoryGeneric<Way>, IRepositoryWay
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
        public IQueryable<Way> GetAllWithInclude()
        {
            return _context.Ways
                .Include(p => p.End)
                .Include(p => p.Start);
        }

        public Way GetAddressId(Way address)
        {
            var addressId = GetAllWithInclude()
                .Where(p => p.Start.Name == address.Start.Name && p.End.Name == address.End.Name)
                .FirstOrDefault();
            return addressId;
        }

    }
}
