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
    public class HikerTripDetailsController : Controller
    {
        private HikerTrailsContext db = new HikerTrailsContext();

        // GET: HikerTripDetails
        public ActionResult Index()
        {
            return View(db.HikerTripDetails.ToList());
        }

        // GET: HikerTripDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HikerTripDetails hikerTripDetails = db.HikerTripDetails.Find(id);
            if (hikerTripDetails == null)
            {
                return HttpNotFound();
            }
            return View(hikerTripDetails);
        }

        // GET: HikerTripDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HikerTripDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,IsHikeCompleted")] HikerTripDetails hikerTripDetails)
        {
            if (ModelState.IsValid)
            {
                db.HikerTripDetails.Add(hikerTripDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hikerTripDetails);
        }

        // GET: HikerTripDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HikerTripDetails hikerTripDetails = db.HikerTripDetails.Find(id);
            if (hikerTripDetails == null)
            {
                return HttpNotFound();
            }
            return View(hikerTripDetails);
        }

        // POST: HikerTripDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserID,IsHikeCompleted")] HikerTripDetails hikerTripDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hikerTripDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hikerTripDetails);
        }

        // GET: HikerTripDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HikerTripDetails hikerTripDetails = db.HikerTripDetails.Find(id);
            if (hikerTripDetails == null)
            {
                return HttpNotFound();
            }
            return View(hikerTripDetails);
        }

        // POST: HikerTripDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HikerTripDetails hikerTripDetails = db.HikerTripDetails.Find(id);
            db.HikerTripDetails.Remove(hikerTripDetails);
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
