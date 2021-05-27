using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastTripApp.BL.Services
{
    public class Util
    {
        public static DateTime TimeNow()
        {
            return DateTime.UtcNow;
        }

        //public static string CurrentUserId()
        //{
        //    return User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}
    }
}
