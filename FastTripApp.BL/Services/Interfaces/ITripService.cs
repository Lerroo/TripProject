using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Reports;
using FastTripApp.DAO.Models.Trip;
using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface ITripService
    {
        Task ToHistory(int? idTrip);
        void Start(int? idTrip);
        void End(int? idTrip);
        void AddNewTripAsync(DefaultTrip trip, Uri uri);
        void UpdateTripAsync(DefaultTrip trip, Uri uri);
        FindMostPopularTrip GetMostPopularTrip(string id);
        IEnumerable<NearestPlaces> GetNearstPlaces(Coords centerCoords, double radiusDistance);
    }
}
