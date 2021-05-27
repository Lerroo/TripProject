
using FastTripApp.DAO;
using FastTripApp.DAO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FastTripApp.Controllers
{
    public class HistoryTripController : Controller
    {
        private readonly UsingIdentityContext _db;
        public HistoryTripController(UsingIdentityContext db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<HistoryTrip> objList = _db.HistoryTrips;
            return View(objList);
        }



    }
}
