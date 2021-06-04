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
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        private readonly ITripService _tripService;        
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;
        private readonly IUnitOfWorkService _unitOfWorkService;        

        public TripController(IRepositoryTrip tripRepository, 
            IRepositoryHistoryTrip historyRepository, 

            ITripService tripService,            
            IUtilService utilService,
            IUserService userService,
            IUnitOfWorkService unitOfWorkService,

            IWebHostEnvironment webHostEnvironment)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;

            _tripService = tripService;            
            _utilService = utilService;
            _userService = userService;
            _unitOfWorkService = unitOfWorkService;

            _webHostEnvironment = webHostEnvironment;
        }
  
        public ActionResult Index()
        {
            var id = _userService.GetCurrentUserId();
            IEnumerable<Trip> objList = _repositoryTrip.GetWithIncludeByUserId(id);

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

                var timeDelay =  (trip.TimeBeforeDeparture.ApproximateStart - _utilService.DateTimeNow()).Value;
                var idJob = BackgroundJob.Schedule(() => _tripService.ToHistory(trip.Id), timeDelay);
                BackgroundJob.ContinueJobWith(
                    idJob, () => Response.Redirect(HttpContext.Request.Path));

                BackgroundJob.Schedule(() => Response.Redirect(Request.Path), timeDelay);
            }
            return View(objList);
        }

        public ActionResult Details(int id)
        {
            var trip = _repositoryTrip.GetWithIncludeById(id);
            if (trip != null)
                return PartialView("_Details", trip);
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
        public async Task<ActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {               
                await _tripService.AddNewTripAsync(trip);                
                
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
            var trip = _repositoryTrip.GetWithIncludeById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {
                 await _tripService.UpdateTripAsync(trip);

                return RedirectToAction("Index");
            }
            return View(trip);
        }

        public ActionResult Abandon(int? id)
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
        public ActionResult AbandonPost(int? id)
        {
            _tripService.End(id);
            return RedirectToAction("Index");
        }
    }
}
