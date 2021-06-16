using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryReview : IRepository<Review>
    {
        IQueryable<Review> GetAllWithInclude();
        Review GetWithIncludeById(int id);
    }
}
