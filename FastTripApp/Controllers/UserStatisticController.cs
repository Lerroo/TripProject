using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastTripApp.Web.Controllers
{
    public class UserStatisticController : Controller
    {
        private readonly IRepositoryHistoryTrip _repositoryHistory;
        private readonly IRepositoryUser _repositoryUser;

        private readonly IUserStatisticService _userStatisticService;
        private readonly IHistoryTripService _historyTripService;

        public UserStatisticController(
            IRepositoryHistoryTrip repositoryHistory,
            IRepositoryUser repositoryUser,
            IUserStatisticService userStatisticService,
            IHistoryTripService historyTripService)
        {
            _repositoryHistory = repositoryHistory;
            _repositoryUser = repositoryUser;
            _userStatisticService = userStatisticService;
            _historyTripService = historyTripService;
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
