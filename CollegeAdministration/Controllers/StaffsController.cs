using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollegeAdministration.Models;

namespace CollegeAdministration.Controllers
{
    
    public class StaffsController : Controller
    {
        private Training_12DecMumbaiEntities db = new Training_12DecMumbaiEntities();

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Course).Include(s => s.Department);
            return View(staffs.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName");
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "staffId,facultyName,emailId,password,gender,address,experience,courseId,departmentId,attendance")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", staff.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", staff.departmentId);
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", staff.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", staff.departmentId);
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staffId,facultyName,emailId,password,gender,address,experience,courseId,departmentId,attendance")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", staff.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", staff.departmentId);
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Staff,Admin")]
        public ActionResult StaffHome()
        {
            Staff staff = (Staff)Session["userStaff"];
            return View(staff);
        }

        [Authorize(Roles = "Staff,Admin")]
        public ActionResult StaffProfilePage()
        {
            Staff staff = (Staff)Session["userStaff"];
            return View(staff);
        }
        [Authorize(Roles = "Staff,Admin")]
        [HttpPost, ActionName("StaffProfilePage")]

        public ActionResult StaffProfilePage([Bind(Include = "staffId,facultyName,emailId,password,gender,address,experience,courseId,departmentId,attendance")] Staff staff)
        {

            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                Session["userStudent"] = staff;
                return RedirectToAction("StaffHome");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", staff.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", staff.departmentId);
            return View(staff);
        }

        //dfhnvgdfhbvifdmhbgjgbjggggggggjgfmijnbgjimbj  nnnnnnnnnijifnbgjindfihbuirnhbtu huir
        //i am trying to retrive student list based up on the couse id of both student and sttaff

        [Authorize(Roles = "Staff,Admin")]
        public ActionResult StaffsStudentAttendancePage()
        {
            Staff staff = (Staff)Session["userStaff"];

            //List<Student> studentList = db.Students.Where(db);
            return View(/*staff*/);
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
