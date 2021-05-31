using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Hangfire.Storage;
using FastTripApp.BL.Services.Interfaces;


namespace FastTripApp.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        private readonly ITripService _tripService;
        private readonly ITimeAfterDepartureService _timeAfterDepartureService;
        private readonly IUtilService _util;

        public TripController(IRepositoryTrip tripRepository, 
            IRepositoryHistoryTrip historyRepository, 

            ITripService tripService,
            ITimeAfterDepartureService timeAfterDepartureService,
            IUtilService util)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;

            _tripService = tripService;
            _timeAfterDepartureService = timeAfterDepartureService;
            _util = util;
        }
  
        public ActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Trip> objList = _repositoryTrip.TripsByUserId(id);

            if (objList.Any())
            {
                var trip = objList.First();
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }

                //fix
                var timeDelay =  (trip.TimeBeforeDeparture.ApproximateStart - _util.DateTimeNow()).Value;
                //var idJob = BackgroundJob.Schedule(() => _tripService.ToHistory(trip.Id), timeDelay);
                //BackgroundJob.ContinueJobWith(
                //    idJob, () => Response.Redirect(HttpContext.Request.Path));
                //    
                BackgroundJob.Schedule(() => Response.Redirect(Request.Path), timeDelay);
            }
            return View(objList);
        }

        public ActionResult Details(int id)
        {
            var historyTrip = _repositoryTrip.GetByIdWithInclude(id);
            if (historyTrip != null)
                return PartialView("_Details", historyTrip);
            return NotFound();
        }

        public ActionResult Start(int id)
        {
            _tripService.Start(id);

            ViewBag.TripId = id;
            return View();
        }

        public ActionResult End(int id)
        {
            _tripService.End(id);
            return RedirectToRoute(new { controller = "Trip", action = "Index" });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _repositoryTrip.Add(trip);
                
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repositoryTrip.GetByIdWithInclude(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trip obj)
        {
            if (ModelState.IsValid)
            {
                _repositoryTrip.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var trip = _repositoryTrip.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int? id)
        {
            _tripService.ToHistory(id);         
            return RedirectToAction("Index");
        }   
    }
}
