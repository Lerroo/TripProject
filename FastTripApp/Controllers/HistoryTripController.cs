using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class HistoryTripController : Controller
    {
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        private readonly IUserService _userService;

        public HistoryTripController(IRepositoryHistoryTrip repositoryHistoryTrip,

            IUserService userService)
        {
            _repositoryHistoryTrip = repositoryHistoryTrip;

            _userService = userService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = _userService.GetCurrentUserId();
            IEnumerable<HistoryTrip> historyTrips = _repositoryHistoryTrip.GetByUserId(userId);
            return View(historyTrips);
        }

        public ActionResult Details(int id)
        {
            var historyTrip = _repositoryHistoryTrip.GetWithIncludeById(id);
            if (historyTrip != null)
                return PartialView("_Details", historyTrip);
            return NotFound();
        }

    }
}
