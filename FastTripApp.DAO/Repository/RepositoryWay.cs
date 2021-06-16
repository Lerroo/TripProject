using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
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

        public Way GetAddressId(Way address)
        {
            var addressId = GetAll()
                .Where(p => p.Start == address.Start && p.End == address.End)
                .FirstOrDefault();
            return addressId;
        }

    }
}
