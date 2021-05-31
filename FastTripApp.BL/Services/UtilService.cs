using FastTripApp.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.BL.Services
{
    
    public class UtilService : IUtilService
    {
        public DateTime DateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
