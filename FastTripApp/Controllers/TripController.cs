using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Models.Reports;
using FastTripApp.DAO.Repository.Interfaces;
using Hangfire;
using Hangfire.Storage;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryCoords _repositoryCoords;

        private readonly ITripService _tripService;
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IReportService _reportService;

        public TripController(IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip historyRepository,
            IRepositoryCoords repositoryCoords,

        ITripService tripService,
            IUtilService utilService,
            IUserService userService,
            IUnitOfWorkService unitOfWorkService,
            IReportService reportService,

            IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment webHostEnvironment)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;
            _repositoryCoords = repositoryCoords;

            _tripService = tripService;
            _utilService = utilService;
            _userService = userService;
            _unitOfWorkService = unitOfWorkService;
            _reportService = reportService;

            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }



        public ActionResult Index()
        {
            var id = _userService.GetCurrentUserId();
            IEnumerable<Trip> objList = _repositoryTrip.GetAllWithIncludeByUserId(id);

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

                //var timeDelay = (trip.TimeBeforeDeparture.ApproximateStart - _utilService.GetDateTimeNow()).Value;
                //var idJob = BackgroundJob.Schedule(() => _tripService.ToHistory(trip.Id), timeDelay);
                //BackgroundJob.ContinueJobWith(
                //    idJob, () => Response.Redirect(HttpContext.Request.Path));

                //BackgroundJob.Schedule(() => Response.Redirect(Request.Path), timeDelay);
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

        public ActionResult FindNearstPlaces()
        {
            var nearestTrip = new NearestPlace()
            {
                CenterCoords = new Coords(),
                RadiusDistance = 20
            };

            var nearstPlaces = _tripService.GetNearstPlaces(nearestTrip);
            nearestTrip.Places = nearstPlaces;

            return View("../Report/NearstPlaces", nearestTrip);
        }

        public ActionResult TripMostPopularReportTemplate()
        {
            var userId = _userService.GetCurrentUserId();
            var mostPopularTrip = _tripService.GetMostPopularTrip(userId);
            return View("../Report/MostPopularTrip", mostPopularTrip);
        }

        public async Task<ActionResult> TripMostPopularPdf()
        {
            var userId = _userService.GetCurrentUserId();
            var mostPopularTrip = _tripService.GetMostPopularTrip(userId);
            CustomPdf file = await _reportService.GetPdfReportAsync(mostPopularTrip, "../Report/MostPopularTrip");
            await _unitOfWorkService.DownloadOnServerAsync(file.FileBytes, userId, "Reports", file.FileName);
            return File(file.FileBytes, "application/pdf");
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
