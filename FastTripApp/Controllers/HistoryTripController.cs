
using FastTripApp.DAO;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Security.Claims;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class HistoryTripController : Controller
    {
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        public HistoryTripController(IRepositoryHistoryTrip repositoryHistoryTrip)
        {
            _repositoryHistoryTrip = repositoryHistoryTrip;
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<HistoryTrip> historyTrips = _repositoryHistoryTrip.HistoryByUserId(userId);
            return View(historyTrips);
        }

        public ActionResult Details(int id)
        {
            var historyTrip = _repositoryHistoryTrip.GetWithInclude(id);
            if (historyTrip != null)
                return PartialView("_Details", historyTrip);
            return NotFound();
        }

    }
}
