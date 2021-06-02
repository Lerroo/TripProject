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

        public DateTime DateTimeNow()
        {
            return DateTime.Now;
        }

        public async Task DownloadAsync(Uri requestUri, string userId, string fileName)
        {
            var path = _unitOfWorkService.PathAndFileName(fileName, userId, "static_way");
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (
                Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
            {
                await contentStream.CopyToAsync(stream);
            }
        }
    }
}
