
using FastTripApp2.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsingIdentity.Data;


namespace FastTripApp2.Controllers
{
    public class TripController : Controller
    {
        private readonly UsingIdentityContext _db;

        public TripController(UsingIdentityContext db)
        {
            _db = db;
        }

        public void TripInHistory(int id, TimeSpan? time)
        {
            var jobId = BackgroundJob.Schedule(() => DeletePost(id), TimeSpan.FromSeconds(time.Value.Seconds));
            var jobId2 = BackgroundJob.Schedule(() => Response.Redirect("Trip/Index"), TimeSpan.FromSeconds(time.Value.Seconds));
        }

        // GET: TripController
        [Authorize]
        public ActionResult Index()
        {           
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Trip> objList = _db.Trips.FromSqlRaw("Select * from Trips where TimePlain>GETDATE() AND UserId='"+id+"'");

            if (objList.GetEnumerator().MoveNext())
            {
                var trip = objList.First();
                //TripInHistory(trip.Id, trip.EstimatedTime);
            }            

            return View(objList);
        }

        // GET: TripController/Start/5
        [Authorize]
        public ActionResult Start()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(Trip obj)
        {
            if (ModelState.IsValid)
            {
                obj.StartTrip = DateTime.UtcNow;
                //fix
                obj.EstimatedTime = new TimeSpan(0, 0, 30);

                _db.Trips.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        
        public ActionResult ToHistory(int id)
        {
            var trip = getById(id);
            HistoryTrip objHistory = new HistoryTrip {
                TripId = trip.Id,
                Name = trip.Name,
                TimePlain = trip.TimePlain,
                EstimatedTime = trip.EstimatedTime,
                Image = trip.Image,
                Descriprion = trip.Descriprion,
                StartTrip = trip.StartTrip,
                EndTrip = trip.EndTrip,
                TimeTrack = trip.TimeTrack,
                AddressStart = trip.AddressStart,
                AddressEnd = trip.AddressEnd,
                AddressEndLatitude = trip.AddressEndLatitude,
                AddressEndLongitude = trip.AddressEndLongitude,
                AddressStartLatitude = trip.AddressStartLatitude,
                AddressStartLongitude = trip.AddressStartLongitude,
                UserId = trip.UserId
            };

            _db.HistoryTrips.Add(objHistory);
            _db.SaveChanges();

            DeleteTripById(id);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult End(int id)
        {
            //fix derirect to create comment
            
            return RedirectToAction("Index");
        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip obj)
        {
            
            if (ModelState.IsValid)
            {
                obj.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _db.Trips.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);                       
        }

        // GET: TripController/Edit
        public ActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Trips.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        // POST: TripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trip obj)
        {
            if (ModelState.IsValid)
            {
                _db.Trips.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: TripController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = getById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: TripController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int? id)
        {
            DeleteTripById(id);
            return RedirectToAction("Index");
        }

        private Trip getById(int? id)
        {
            return _db.Trips.Find(id);
        }

        private void DeleteTripById(int? id)
        {
            var obj = _db.Trips.Find(id);

            //fixxxxxxxxxxxxxxx
            //if (obj == null)
            //{
            //    return NotFound();
            //}

            _db.Trips.Remove(obj);
            _db.SaveChanges();
        }
    }
}
