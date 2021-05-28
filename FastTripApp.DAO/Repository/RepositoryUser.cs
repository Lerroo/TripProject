using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    class RepositoryUser : RepositoryGeneric<User>, IRepositoryUser
    {
        private readonly UsingIdentityContext _context;
        public RepositoryUser(UsingIdentityContext context):base(context)
        {
            _context = context;
        }
    }
}
