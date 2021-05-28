using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Infrastructure
{
    public interface IUnitOfWork
    {
        void UploadImage(IFormFile file);
    }
}
