using FastTripApp.BL.Services.Interfaces;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IRepositoryReview _repositoryReview;
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;

        private readonly ITripService _tripService;
        
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;

        public ReviewController(IRepositoryTrip tripRepository,
            IRepositoryHistoryTrip historyRepository,
            IRepositoryReview repositoryReview,

            ITripService tripService,
            
            IUtilService utilService,
            IUserService userService)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;

            _tripService = tripService;
            
            _utilService = utilService;
            _userService = userService;
            _repositoryReview = repositoryReview;
        }

        // GET: ReviewController
        public ActionResult Index()
        {
            List<Review> objList = _repositoryReview.GetWithInclude().ToList();
            return View(objList);
        }

        public ActionResult GetComments(int id)
        {
            var la = _repositoryReview.GetById(id);
            List<Comment> comments = la.Comments;
            return PartialView("../Comment/_Index", comments);
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: ReviewController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserId = _userService.GetCurrentUserId();
                review.TimePost = _utilService.DateTimeNow();

                _repositoryReview.Add(review);
                return RedirectToRoute(new { controller = "Trip", action = "End", id = review.TripId });
            }

            return View("Trip/Start/", review);
        }
    }
}
