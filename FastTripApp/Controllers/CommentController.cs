using FastTripApp.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.BL.Services.Interfaces;

namespace FastTripApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IRepositoryComment _repositoryComment;

        private readonly IUtilService _utilService;
        private readonly IUserService _userService;

        public CommentController(
            IRepositoryComment repositoryComment, 

            IUtilService utilService,
            IUserService userService)
        {
            _repositoryComment = repositoryComment;

            _utilService = utilService;
            _userService = userService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = _userService.GetCurrentUserId();
                comment.TimePost = _utilService.DateTimeNow();

                _repositoryComment.Add(comment);
                return RedirectToRoute(new { controller = "Review", action = "Index" });
            }
            return View(comment);
        }
    }
}
