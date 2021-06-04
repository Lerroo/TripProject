using System;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUtilService
    {
        DateTime DateTimeNow();
        Task DownloadAsync(Uri requestUri, string userId, string fileName);
    }
}
