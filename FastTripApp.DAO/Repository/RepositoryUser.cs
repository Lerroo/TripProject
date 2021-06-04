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

        public RepositoryUser(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to get one user object from the repository.
        /// </summary>
        /// <param name="id">Special unique identifier for user in the repository.</param>
        /// <returns>Returns user object with a specified type by ID from the repository.</returns>
        public UserCustom GetById(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
