using FastTripApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastTripApp.Web.Controllers
{
    public class UserStatisticController : Controller
    {
        private readonly IUserStatisticService _userStatisticService;

        public UserStatisticController(IUserStatisticService userStatisticService)
        {
            _userStatisticService = userStatisticService;
        }

        // GET: StatisticController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTripStatistic = _userStatisticService.GetByUserId(userId);        
   
            return View(userTripStatistic);
        }
    }
}
