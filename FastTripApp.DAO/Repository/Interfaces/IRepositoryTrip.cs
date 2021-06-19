using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryTrip : IRepository<DefaultTrip>
    {
        DefaultTrip GetWithIncludeById(int? id);
        IQueryable<DefaultTrip> GetAllWithIncludeByUserId(string id);
    }
}
