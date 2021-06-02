using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryUser : RepositoryGeneric<UserCustom>, IRepositoryUser
    {
        private readonly UsingIdentityContext _context;
        public RepositoryUser(UsingIdentityContext context):base(context)
        {
            _context = context;
        }

        public UserCustom GetById(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
