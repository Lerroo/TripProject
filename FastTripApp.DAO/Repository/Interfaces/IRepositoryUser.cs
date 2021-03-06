using FastTripApp.DAO.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryUser : IRepository<UserCustom>
    {
        UserCustom GetById(string id);
    }
}
