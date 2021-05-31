using FastTripApp.DAO.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class UtilService : IUtilService
    {
        public DateTime DateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
