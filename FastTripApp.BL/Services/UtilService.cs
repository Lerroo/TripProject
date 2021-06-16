using FastTripApp.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services
{    
    public class UtilService : IUtilService
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public UtilService(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        /// <summary>
        /// Returns current computer time
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Asynchronously downloads a picture from {requestUri} to a folder ~/uploads/users/{userId}/{fileName}.
        /// </summary>
        /// <param name="requestUri">Link where to download the picture.</param>
        /// <param name="userId">Id user for which you need to download a picture.</param>
        /// <param name="fileName">Name folder to download a picture.</param>
        /// <returns></returns>
        public async Task DownloadUriContentAsync(Uri requestUri, string userId, string fileName)
        {
            var path = _unitOfWorkService.PathAndFileName(fileName, "way_static_images");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (
                Stream contentStream = 
                    await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                    stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
            {
                await contentStream.CopyToAsync(stream);
            }
        }
    }
}
