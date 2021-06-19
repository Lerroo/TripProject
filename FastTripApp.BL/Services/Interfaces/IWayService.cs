
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IWayService
    {
        IQueryable<Place> GetNearstPlaces();
    }
}
