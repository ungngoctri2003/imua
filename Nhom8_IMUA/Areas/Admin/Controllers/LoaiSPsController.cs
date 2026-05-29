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
    public class LoaiSPsController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/LoaiSPs
        [HasCredential(RoleID = "VIEW_LOAISP")]
        public ActionResult Index(int? page)
        {
            var loaiSP = db.LoaiSPs.Select(p => p).OrderBy(s => s.MaLoai);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(loaiSP.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LoaiSPs/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var loaiSP = db.LoaiSPs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                loaiSP = loaiSP.Where(p => p.TenLoai.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            loaiSP = loaiSP.OrderBy(p => p.MaLoai);
            ViewData["Count"] = loaiSP.Count().ToString();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(loaiSP.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LoaiSPs/Details/5
        [HasCredential(RoleID = "VIEW_LOAISP")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Create
        [HasCredential(RoleID = "ADD_LOAISP")]
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            return View();
        }

        // POST: Admin/LoaiSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoai,TenLoai,MaDM")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSPs.Add(loaiSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", loaiSP.MaDM);
            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Edit/5
        [HasCredential(RoleID = "EDIT_LOAISP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", loaiSP.MaDM);
            return View(loaiSP);
        }

        // POST: Admin/LoaiSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoai,TenLoai,MaDM")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", loaiSP.MaDM);
            return View(loaiSP);
        }

        [HasCredential(RoleID = "DELETE_LOAISP")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            db.LoaiSPs.Remove(loaiSP);
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
