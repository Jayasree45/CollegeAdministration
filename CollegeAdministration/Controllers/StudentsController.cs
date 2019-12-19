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
    public class StudentsController : Controller
    {
        private Training_12DecMumbaiEntities db = new Training_12DecMumbaiEntities();
        [Authorize(Roles = "Admin")]
        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Course).Include(s => s.Department);
            return View(students.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName");
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName");
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentId,studentName,emailId,phoneNumber,password,address,courseId,departmentId,gender,attendance,academicYear,percentage")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", student.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", student.departmentId);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", student.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", student.departmentId);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,studentName,emailId,phoneNumber,password,address,courseId,departmentId,gender,attendance,academicYear,percentage")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", student.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", student.departmentId);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Student,Admin")]
        public ActionResult StudentHome()
        {

            Student stu = (Student)Session["userStudent"];
            return View(stu);
        }

        [Authorize(Roles = "Student,Admin")]
        public ActionResult StudentProfilePage()
        {

            Student stu = (Student)Session["userStudent"];
            return View(stu);
        }
        [Authorize(Roles = "Student,Admin")]
        [HttpPost, ActionName("StudentProfilePage")]

        public ActionResult StudentProfilePage([Bind(Include = "studentId,studentName,emailId,phoneNumber,password,address,courseId,departmentId,gender")] Student student)
        {

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                Session["userStudent"]= student;
                return RedirectToAction("StudentHome");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", student.courseId);
            ViewBag.departmentId = new SelectList(db.Departments, "departmentId", "departmentName", student.departmentId);
            return View(student);
        }
        [Authorize(Roles = "Student,Admin")]
        public ActionResult StudentAttendancePage()
        {

            Student stu = (Student)Session["userStudent"];
            return View(stu);
        }
        [Authorize(Roles = "Student,Admin")]
        public ActionResult StudentMarksPage()
        {

            Student stu = (Student)Session["userStudent"];
            return View(stu);
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
