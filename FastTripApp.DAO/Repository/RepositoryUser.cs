using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryUser : RepositoryGeneric<User>, IRepositoryUser
    {
        private readonly UsingIdentityContext _context;
        public RepositoryUser(UsingIdentityContext context):base(context)
        {
            _context = context;
        }

        public User GetById(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
