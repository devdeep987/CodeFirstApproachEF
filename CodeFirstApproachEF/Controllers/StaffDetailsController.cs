using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstApproachEF.Models;

namespace CodeFirstApproachEF.Controllers
{
    [Authorize]
    public class StaffDetailsController : Controller
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: StaffDetails
        public ActionResult Index(string ID)
        {
            int id = Convert.ToInt32(ID);
            return View(db.StaffDetails.Where(s=>s.courseId == id || id==0).ToList());
        }

        // GET: StaffDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            return View(staffDetail);
        }

        // GET: StaffDetails/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName");
            return View();
        }

        // POST: StaffDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,Name,courseId")] StaffDetail staffDetail)
        {
            if (ModelState.IsValid)
            {
                db.StaffDetails.Add(staffDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", staffDetail.courseId);
            return View(staffDetail);
        }

        // GET: StaffDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", staffDetail.courseId);
            return View(staffDetail);
        }

        // POST: StaffDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,Name,courseId")] StaffDetail staffDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", staffDetail.courseId);
            return View(staffDetail);
        }

        // GET: StaffDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            return View(staffDetail);
        }

        // POST: StaffDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            db.StaffDetails.Remove(staffDetail);
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
