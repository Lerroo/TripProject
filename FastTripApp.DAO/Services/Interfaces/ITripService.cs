using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Services.Interfaces
{
    public interface ITripService
    {
        Task ToHistory(int? id);
        void Start(int id);
        void End(int id);
    }
}
