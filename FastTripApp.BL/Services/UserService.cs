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
        private UserManager<UserCustom> _userManager;

        public UserService(IHttpContextAccessor accessor,
            UserManager<UserCustom> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public UserCustom GetUser()
        {
            return _userManager.GetUserAsync(ClaimsPrincipal.Current).Result;
        }

        public ClaimsPrincipal GetClaims()
        {
            return _accessor?.HttpContext?.User as ClaimsPrincipal;
        }

        public string GetCurrentUserId()
        {
            var userId = GetClaims().Claims.ElementAt(0).Value;
            return userId;
        }

        public string GetUserPath(string id)
        {
            return "/uploads/users/" + id;
        }
    }
}