using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using CollegeAdministration.Models;

namespace CollegeAdministration.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult RoleList()
        {
            var roleList = _db.Roles.ToList();
            return View(roleList);
        }


        [HttpGet]
        // GET: Role
        public ActionResult CreateRole()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        [HttpPost]
        public ActionResult CreateRole(IdentityRole identity)
        {
            _db.Roles.Add(identity);
            _db.SaveChanges();
            return RedirectToAction("RoleList");
        }
        public ActionResult AdminHomePage()
        {
            return View();
        }
    }
}