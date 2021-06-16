using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryWay : IRepository<Way>
    {
        Way GetAddressId(Way address);
    }
}
