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

        private readonly IUtilService _util;

        public CommentController(
            IRepositoryComment repositoryComment, 

            IUtilService util)
        {
            _repositoryComment = repositoryComment;

            _util = util;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.TimePost = _util.DateTimeNow();

                _repositoryComment.Add(comment);
                return RedirectToRoute(new { controller = "Review", action = "Index" });
            }
            return View(comment);
        }
    }
}
