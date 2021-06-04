
using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web.Providers.Entities;

namespace FastTripApp.BL.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        /// <summary>
        /// Method of upload an image from a form to a server.
        /// </summary>
        /// <param name="file">
        /// Image file to upload to server.
        /// </param>
        /// <param name="userId">
        /// Upload avatar to user by userId.
        /// </param>
        public async void UploadImage(IFormFile file, string userId)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = EnsureFileName(fileName);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = File.Create(PathAndFileName(fileName, userId, "avatars")))
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

        /// <summary>
        /// Method to get file name and type without full path.
        /// </summary>
        /// <param name="fileName">Path with file name.</param>
        /// <returns>File name and type without full path.</returns>
        private string EnsureFileName(string fileName)
        {
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;
        }

        /// <summary>
        /// Method to get clear path and checking the existence of a path to save a file.
        /// </summary>
        /// <param name="fileName">
        /// Name and type file
        /// </param>
        /// <param name="userId">Upload file to user by userId.</param>
        /// <param name="folder">Name of the folder where to upload the file</param>
        /// <returns>Clear path to upload</returns>
        public string PathAndFileName(string fileName, string userId, string folder)
        {
            string path = Path.Combine("wwwroot\\uploads\\users\\", userId, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, fileName);
        }
    }
}
