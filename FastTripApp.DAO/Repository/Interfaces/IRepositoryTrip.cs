using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryTrip : IRepository<Trip>
    {
        Trip GetWithIncludeById(int? id);
        IQueryable<Trip> GetAllWithIncludeByUserId(string id);
    }
}
