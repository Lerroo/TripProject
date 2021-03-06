using Microsoft.AspNetCore.Http;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        void UploadImage(IFormFile file, string userId);
        string PathAndFileName(string fileName, string userId, string folder);
    }
}
