using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryComment : RepositoryGeneric<Comment>, IRepositoryComment
    {
        private readonly UsingIdentityContext _context;

        public RepositoryComment(UsingIdentityContext context) : base(context)
        {
            _context = context;
        }
    }
}
