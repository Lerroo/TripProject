using FastTripApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FastTripApp.Web.Controllers
{
    [Authorize]
    public class UserStatisticController : Controller
    {
        private readonly IUserStatisticService _userStatisticService;
        private readonly IUserService _userService;

        public UserStatisticController(IUserStatisticService userStatisticService,
            IUserService userService)
        {
            _userStatisticService = userStatisticService;
            _userService = userService;
        }

        // GET: StatisticController
        public ActionResult Index(int currentYear)
        {
            var userId = _userService.GetCurrentUserId();

            var userTripStatistic = _userStatisticService.GetByYear(currentYear, userId);        
   
            return View(userTripStatistic);
        }

    }
}
