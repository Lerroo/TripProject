using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryReview : RepositoryGeneric<Review>, IRepositoryReview
    {
        private readonly UsingIdentityContext _сontext;
        public RepositoryReview(UsingIdentityContext usingIdentityContext) : base(usingIdentityContext)
        {
            _сontext = usingIdentityContext;
        }

        List<Review> IRepositoryReview.Get()
        {
            return _сontext.Reviews.Include(p => p.Comments).ToList();
        }
    }
}
