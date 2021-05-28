using FastTripApp.DAO.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IHostingEnvironment _hostingEnvironment;

        public UnitOfWork(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async void UploadImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = EnsureFileName(fileName);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = File.Create(PathAndFileName(fileName)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }

        private string EnsureFileName(string fileName)
        {
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;

            //2141Ddsa*
        }        

        private string PathAndFileName(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\uploads\\profile_pic\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + fileName;
        }
    }
}
