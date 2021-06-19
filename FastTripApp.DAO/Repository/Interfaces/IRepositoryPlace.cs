﻿using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryPlace : IRepository<Place>
    {
        IQueryable<Place> GetAllWithInclude();
    }
}
