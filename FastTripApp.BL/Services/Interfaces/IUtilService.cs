using System;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUtilService
    {
        DateTime GetDateTimeNow();
        string GetGuid();
        Task DownloadUriContentAsync(Uri requestUri, string userId, string fileName);
    }
}
