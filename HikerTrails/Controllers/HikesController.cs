using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HikerTrails.Models;

namespace HikerTrails.Controllers
{
    [Authorize(Roles = "admin")]
    public class HikesController : Controller
    {
        private HikerTrailsContext db = new HikerTrailsContext();

        // GET: Hikes
        public ActionResult Index()
        {
            return View(db.Hikes.ToList());
        }

        // GET: Hikes/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Cost,Difficulty,MinimumAge,Duration,StartDate,EndDate")] Hikes hikes)
        {
            if (ModelState.IsValid)
            {
                db.Hikes.Add(hikes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hikes);
        }

        // GET: Hikes/Edit/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Hikes hikes = db.Hikes.Find(id);
            db.Hikes.Remove(hikes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
