using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryPlace : RepositoryGeneric<Place>, IRepositoryPlace
    {
        private readonly UsingIdentityContext _context;

        public RepositoryPlace(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Place> GetAllWithInclude()
        {
            return _context.Places
                .Include(p => p.Coords);
        }
    }
}
