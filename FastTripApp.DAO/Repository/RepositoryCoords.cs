using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryCoords : RepositoryGeneric<Coords>, IRepositoryCoords
    {
        private readonly UsingIdentityContext _context;

        public RepositoryCoords(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }
    }
}
