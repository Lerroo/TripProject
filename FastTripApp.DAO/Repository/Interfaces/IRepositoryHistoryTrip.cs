using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryHistoryTrip : IRepository<HistoryTrip>
    {
        HistoryTrip GetWithIncludeById(int id);
        IQueryable<HistoryTrip> GetByUserId(string id);
    }
}
