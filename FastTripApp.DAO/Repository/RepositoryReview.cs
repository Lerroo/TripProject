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

        /// <summary>
        /// Method to get list of all Review objects with includes from the repository.
        /// </summary>
        /// <returns>
        /// Returns list of Review objects with includes from the repository.
        /// </returns>
        public IQueryable<Review> GetAllWithInclude()
        {
            return _сontext.Reviews
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Include(p => p.User);
        }

        /// <summary>
        /// Method to get one Review object with includes from the repository.
        /// </summary>
        /// <param name="reviewId">
        /// Special unique identifier for Review in the repository.
        /// </param>
        /// <returns>
        /// Returns Review object with includes by id from the repository.
        /// </returns>
        public Review GetWithIncludeById(int reviewId)
        {
            return GetAllWithInclude()
                .FirstOrDefault(p => p.ReviewId == reviewId);
        }
    }
}