using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Nhom8_IMUA.Common;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Areas.Admin.Controllers
{
    public class VaiTrosController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/VaiTros
        [HasCredential(RoleID = "VIEW_ROLE")]
        public ActionResult Index(int? page)
        {
            var vaiTro = db.Roles.Select(p => p).OrderBy(s => s.Name);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(vaiTro.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/VaiTros/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var vaiTro = db.Roles.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                vaiTro = vaiTro.Where(p => p.Name.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            vaiTro = vaiTro.OrderBy(p => p.RoleID);
            ViewData["Count"] = vaiTro.Count().ToString();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(vaiTro.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/VaiTros/Details/5
        [HasCredential(RoleID = "VIEW_ROLE")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Admin/VaiTros/Create
        [HasCredential(RoleID = "ADD_ROLE")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VaiTros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Admin/VaiTros/Edit/5
        [HasCredential(RoleID = "EDIT_ROLE")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/VaiTros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Admin/VaiTros/Delete/5
        [HasCredential(RoleID = "DELETE_ROLE")]
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Role role = db.Roles.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //// POST: Admin/VaiTros/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Role role = db.Roles.Find(id);
        //    db.Roles.Remove(role);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public JsonResult Delete(string id)
        {
            Role vaiTro = db.Roles.SingleOrDefault(x => x.RoleID == id);
            db.Roles.Remove(vaiTro);
            db.SaveChanges();
            return Json(new { ThongBao = "successs" });
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
