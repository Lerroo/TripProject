using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface ITripService
    {
        Task ToHistory(int? id);
        void Start(int id);
        void End(int id);
    }
}
