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
    public class studentDetailsController : Controller
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: studentDetails
        public ActionResult Index(string ID)
        {
            int id = Convert.ToInt32(ID);
            return View(db.studentDetails.Where(s=>s.courseId == id || id==0).ToList());
        }

        // GET: studentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetail studentDetail = db.studentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // GET: studentDetails/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName");
            return View();
        }

        // POST: studentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,RollNumber,courseId")] studentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.studentDetails.Add(studentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", studentDetail.courseId);
            return View(studentDetail);
        }

        // GET: studentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetail studentDetail = db.studentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", studentDetail.courseId);
            return View(studentDetail);
        }

        // POST: studentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,RollNumber,courseId")] studentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.courses, "CourseID", "CourseName", studentDetail.courseId);
            return View(studentDetail);
        }

        // GET: studentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetail studentDetail = db.studentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: studentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentDetail studentDetail = db.studentDetails.Find(id);
            db.studentDetails.Remove(studentDetail);
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
