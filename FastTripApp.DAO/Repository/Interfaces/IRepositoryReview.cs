﻿using FastTripApp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepositoryReview : IRepository<Review>
    {
        new List<Review> Get();
    }
}
