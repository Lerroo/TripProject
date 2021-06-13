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
        
        private readonly IUtilService _utilService;
        private readonly IUserService _userService;

        public ReviewController(IRepositoryReview repositoryReview,
            
            IUtilService utilService,
            IUserService userService)
        {
            _repositoryReview = repositoryReview;

            _utilService = utilService;
            _userService = userService;
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
                review.TimePost = _utilService.GetDateTimeNow();

                _repositoryReview.Add(review);
                return RedirectToRoute(new { controller = "Trip", action = "End", id = review.TripId });
            }

            return View("Trip/Start/", review);
        }
    }
}
