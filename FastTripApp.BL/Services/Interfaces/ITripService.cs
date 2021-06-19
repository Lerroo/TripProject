using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Reports;
using FastTripApp.DAO.Models.Trip;
using FastTripApp.DAO.Models.Trip.Way;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface ITripService
    {
        Task ToHistory(int? idTrip);
        void Start(int? idTrip);
        void End(int? idTrip);
        Task AddNewTripAsync(DefaultTrip trip);
        Task UpdateTripAsync(DefaultTrip trip);
        MostPopularTrip GetMostPopularTrip(string id);
        IEnumerable<Place> GetNearstPlaces(NearestPlace centerPlace);
    }
}
