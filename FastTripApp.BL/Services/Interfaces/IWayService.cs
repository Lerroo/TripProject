
using FastTripApp.DAO.Models;
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
