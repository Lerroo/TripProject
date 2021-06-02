using FastTripApp.DAO.Models.Identity;
using System.Security.Claims;

namespace FastTripApp.BL.Services.Interfaces
{
    public interface IUserService
    {
        UserCustom GetUser();
        string GetCurrentUserId();
        ClaimsPrincipal GetClaims();
    }
}