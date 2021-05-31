using FastTripApp.DAO.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryUser : IRepository<User>
    {
        User GetUserById(string id);
    }
}
