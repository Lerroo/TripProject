﻿using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryWay : IRepository<Way>
    {
        IQueryable<Way> GetAllWithInclude();
        Way GetAddressId(Way address);
    }
}
