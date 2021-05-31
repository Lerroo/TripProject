using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryReview : IRepository<Review>
    {
        IEnumerable<Review> GetWithInclude();
        Review GetByIdWithInclude(int id);
    }
}
