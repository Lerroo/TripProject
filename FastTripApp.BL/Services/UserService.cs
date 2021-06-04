using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace FastTripApp.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Methot to get ClaimsPrincipal object from HttpContext.
        /// </summary>
        /// <returns>Return ClaimsPrincipal object for current login user.</returns>
        public ClaimsPrincipal GetClaims()
        {
            return _accessor?.HttpContext?.User as ClaimsPrincipal;
        }

        /// <summary>
        /// Method for getting the string value of id current login user.
        /// </summary>
        /// <returns>String value of id current login user.</returns>
        public string GetCurrentUserId()
        {
            var userId = GetClaims().Claims.ElementAt(0).Value;
            return userId;
        }
    }
}