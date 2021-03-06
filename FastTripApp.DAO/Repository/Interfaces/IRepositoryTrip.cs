using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryTrip : IRepository<Trip>
    {
        IQueryable<Trip> GetAllWithInclude();
        Trip GetWithIncludeById(int? id);
        IQueryable<Trip> GetWithIncludeByUserId(string id);
    }
}
