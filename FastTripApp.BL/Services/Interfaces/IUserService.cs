using FastTripApp.DAO.Models.Identity;
using System.Security.Claims;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUserService
    {
        string GetCurrentUserId();
        ClaimsPrincipal GetClaims();
        UserCustom GetCurrentUser();
    }
}