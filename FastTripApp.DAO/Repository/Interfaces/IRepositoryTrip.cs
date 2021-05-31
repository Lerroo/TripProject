using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryTrip : IRepository<Trip>
    {
        Trip GetByIdWithInclude(int? id);
        IEnumerable<Trip> TripsByUserId(string id);
        void SetStatus(int id);
    }
}
