using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HikerTrails.Models;
using HikerTrails.Utils;
using Microsoft.AspNet.Identity;

namespace HikerTrails.Controllers
{
    public class HikesController : Controller
    {
        private HikerTrailsContext db = new HikerTrailsContext();
        //private ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Hikes
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Hikes.ToList());
        }

        // GET: Hikes/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hikes hikes = db.Hikes.Find(id);
            if (hikes == null)
            {
                return HttpNotFound();
            }
            return View(hikes);
        }

        // GET: Hikes/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Cost,Difficulty,MinimumAge,Duration,StartDate,EndDate,StartLongitude,StartLatitude,MaximumParticipants")] Hikes hikes, 
            HttpPostedFileBase postedImage)
        {
            ModelState.Clear();
            hikes.ImagePath = string.Format(@"{0}", Guid.NewGuid());

            StringBuilder strDesc = new StringBuilder();
            strDesc.Append(HttpUtility.HtmlEncode(hikes.Description));
            hikes.Description = strDesc.ToString();

            TryValidateModel(hikes);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExt = Path.GetExtension(postedImage.FileName);
                string filePath = hikes.ImagePath + fileExt;
                hikes.ImagePath = filePath;
                postedImage.SaveAs(serverPath + hikes.ImagePath);

                //var startDateStr = hikes.StartDate.ToString("yyyy-MM-dd hh:mm");
                //var startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                //var endDateStr = hikes.EndDate.ToString("yyyy-MM-dd hh:mm");
                //var endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);

                db.Hikes.Add(hikes);
                db.SaveChanges();

                ApplicationDbContext UsersContext = new ApplicationDbContext();
                var role = UsersContext.Roles.SingleOrDefault(m => m.Name == "hiker");
                var users = UsersContext.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id)).ToList();
                EmailSender es = new EmailSender();
                var subject = "We have a new hike you might be interested in!";
                var contents = "Hike name: " + hikes.Name;
                for(int i = 0; i< users.Count(); i++)
                {
                    es.Send(users[i].Email, subject, contents, serverPath + hikes.ImagePath);
                }
                return RedirectToAction("Index");
            }

            return View(hikes);
        }

        // GET: Hikes/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hikes hikes = db.Hikes.Find(id);
            if (hikes == null)
            {
                return HttpNotFound();
            }
            return View(hikes);
        }

        // POST: Hikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Cost,Difficulty,MinimumAge,Duration,StartDate,EndDate")] Hikes hikes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hikes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hikes);
        }

        // GET: Hikes/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hikes hikes = db.Hikes.Find(id);
            if (hikes == null)
            {
                return HttpNotFound();
            }
            return View(hikes);
        }

        // POST: Hikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Hikes hikes = db.Hikes.Find(id);
            var hikeTrips = db.HikerTripDetails.Where(x => x.HikeId == id).ToList();
            for (int i=0; i<hikeTrips.Count; i++)
            {
                db.HikerTripDetails.Remove(hikeTrips[i]);
            }
            db.Hikes.Remove(hikes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ShowHikes()
        {
            return View(db.Hikes.ToList());
        }

        public ActionResult ViewHike(int? id)
        {
            Boolean isHikerRegistered = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hikes hikes = db.Hikes.Find(id);
            var currentUserId = User.Identity.GetUserId();
            var hikeTrips = db.HikerTripDetails.Where(x => x.UserID == currentUserId && x.HikeId == id).ToList();
            if (hikeTrips.Count > 0) {
                isHikerRegistered = true;
            }
            if (hikes == null)
            {
                return HttpNotFound();
            }
            ViewBag.isHikeRegistered = isHikerRegistered;
            return View(hikes);
        }

        [Authorize(Roles = "hiker")]
        public ActionResult RegisterForHike(int? id)
        {
            ModelState.Clear();
            Hikes hikes = db.Hikes.Find(id);
            TryValidateModel(hikes);
            if (ModelState.IsValid)
            {
                hikes.RegisteredParticipants = hikes.RegisteredParticipants + 1;
                HikerTripDetails hikertrip = new HikerTripDetails();
                hikertrip.HikeId = hikes.Id;
                hikertrip.UserRating = 0;
                hikertrip.UserID = User.Identity.GetUserId();
                db.Hikes.Attach(hikes);
                db.Entry(hikes).Property(x => x.RegisteredParticipants).IsModified = true;
                db.HikerTripDetails.Add(hikertrip);
                db.SaveChanges();
                return RedirectToAction("ShowHikes");
            }

            return View(hikes);
        }

        public ActionResult ViewCalendar()
        {
            return View(db.Hikes.ToList());
        }

        public ActionResult ViewMap()
        {
            return View(db.Hikes.ToList());
        }

        [Authorize(Roles = "hiker")]
        public ActionResult ShowRatingHikes()
        {
            var currentUserId = User.Identity.GetUserId();
            var hikeTrips = db.HikerTripDetails.Where(x => x.UserID == currentUserId && x.UserRating == 0).ToList();
            List<int> hikeIds = new List<int>();
            for(int i=0 ; i < hikeTrips.Count ; i++)
            {
                hikeIds.Add(hikeTrips[i].HikeId);
            }
            DateTime currentDate = DateTime.Now;
            var hikes = db.Hikes.Where(x => hikeIds.Contains(x.Id) && x.EndDate < currentDate).ToList();
            
            return View(hikes);
        }

        [Authorize(Roles = "hiker")]
        public ActionResult RateHike(int? id)
        {
            return View(db.Hikes.Find(id));
        }

        [Authorize(Roles = "hiker")]
        public ActionResult RateHikeCalculate(int? id, string rating)
        {
            var hikeTrips = db.HikerTripDetails.Where(x => x.HikeId == id && x.UserRating > 0).ToList();
            float sumRating = 0;
            for (int i = 0; i < hikeTrips.Count; i++)
            {
                sumRating = sumRating + hikeTrips[i].UserRating;
            }
            sumRating = sumRating + float.Parse(rating);
            var finalRating = sumRating / (hikeTrips.Count + 1);

            Hikes hike = db.Hikes.Find(id);
            hike.HikeRating = finalRating;

            var currentUserId = User.Identity.GetUserId();
            HikerTripDetails hikerTrip = db.HikerTripDetails.Where(x => x.HikeId == id && x.UserID == currentUserId).ToList()[0];
            hikerTrip.UserRating = float.Parse(rating);

            db.Hikes.Attach(hike);
            db.Entry(hike).Property(x => x.HikeRating).IsModified = true;
            db.HikerTripDetails.Attach(hikerTrip);
            db.Entry(hikerTrip).Property(x => x.UserRating).IsModified = true;
            db.SaveChanges();

            return RedirectToAction("ShowRatingHikes");
        }

        public ActionResult ShowChart()
        {
            return View(db.Hikes.ToList());
        }
    }
}
