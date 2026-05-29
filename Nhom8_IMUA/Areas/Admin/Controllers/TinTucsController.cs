using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Nhom8_IMUA.Models;
using Nhom8_IMUA.Common;

namespace Nhom8_IMUA.Areas.Admin.Controllers
{
    public class TinTucsController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/TinTucs
        [HasCredential(RoleID = "VIEW_TINTUC")]
        public ActionResult Index(int? page)
        {
            var tinTuc = db.TinTucs.Select(p => p).OrderBy(s => s.MaTinTuc);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tinTuc.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPhams/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var tinTuc = db.TinTucs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                tinTuc = tinTuc.Where(p => p.TieuDe.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            tinTuc = tinTuc.OrderBy(p => p.MaTinTuc);
            ViewData["Count"] = tinTuc.Count().ToString();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tinTuc.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TinTucs/Details/5
        [HasCredential(RoleID = "VIEW_TINTUC")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        [HasCredential(RoleID = "ADD_TINTUC")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinTuc,TieuDe,TomTat,NoiDung,AnhTinTuc")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                //tinTuc.AnhTinTuc = "";
                var f = Request.Files["AnhTinTuc"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/assets/Images/TinTuc/" + FileName);
                    f.SaveAs(UploadPath);
                    tinTuc.AnhTinTuc = FileName;
                }
                else
                {
                    tinTuc.AnhTinTuc = "default-img.png";
                }
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Edit/5
        [HasCredential(RoleID = "EDIT_TINTUC")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinTuc,TieuDe,TomTat,NoiDung,AnhTinTuc")] TinTuc tinTuc)
        {

            if (ModelState.IsValid)
            {
                //tinTuc.AnhTinTuc = "";
                var f = Request.Files["AnhTinTuc"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/assets/Images/TinTuc/" + FileName);
                    f.SaveAs(UploadPath);
                    tinTuc.AnhTinTuc = FileName;
                }
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TinTuc tinTuc = db.TinTucs.Find(id);
        //    if (tinTuc == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tinTuc);
        //}

        // POST: Admin/TinTucs/Delete/5
        [HasCredential(RoleID = "DELETE_TINTUC")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
