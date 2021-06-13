using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        Task UploadImageAsync(IFormFile file, string userId);
        string PathAndFileName(string fileName, string userId, string folder);
        Task DownloadOnServerAsync(byte[] file, string userId, string folderUrl, string fileName);
    }
}
