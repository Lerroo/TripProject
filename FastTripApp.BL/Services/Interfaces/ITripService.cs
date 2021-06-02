using FastTripApp.DAO.Models;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface ITripService
    {
        Task ToHistory(int? idTrip);
        void Start(int? idTrip);
        void End(int? idTrip);
        void AddNewTrip(Trip trip);
        void UpdateTrip(Trip trip);
    }
}
