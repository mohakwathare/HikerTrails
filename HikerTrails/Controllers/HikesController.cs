using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public ActionResult Create([Bind(Include = "Id,Name,Description,Cost,Difficulty,MinimumAge,Duration,StartDate,EndDate,MaximumParticipants")] Hikes hikes, 
            HttpPostedFileBase postedImage)
        {
            ModelState.Clear();
            hikes.ImagePath = string.Format(@"{0}", Guid.NewGuid());
            TryValidateModel(hikes);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExt = Path.GetExtension(postedImage.FileName);
                string filePath = hikes.ImagePath + fileExt;
                hikes.ImagePath = filePath;
                postedImage.SaveAs(serverPath + hikes.ImagePath);

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

        [Authorize(Roles = "admin,hiker")]
        public ActionResult ViewHike(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,hiker")]
        public ActionResult RegisterForHike([Bind(Include = "Id")] Hikes hikes)
        { 

            if (ModelState.IsValid)
            {
                HikerTripDetails hikertrip = new HikerTripDetails();
                hikertrip.Hike = db.Hikes.Find(hikes.Id);
                hikertrip.IsHikeCompleted = false;
                hikertrip.UserID = User.Identity.GetUserId();
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
    }
}
