using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryReview : IRepository<DefaultReview>
    {
        IQueryable<DefaultReview> GetAllWithInclude();
        DefaultReview GetWithIncludeById(int id);
    }
}
