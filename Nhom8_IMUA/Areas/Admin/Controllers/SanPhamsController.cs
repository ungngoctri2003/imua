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
    public class SanPhamsController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/SanPhams
        [HasCredential(RoleID = "VIEW_SANPHAM")]
        public ActionResult Index(int? page)
        {
            var sanPham = db.SanPhams.Select(p => p).OrderBy(s => s.MaSP);
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanPham.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPhams/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var sanPham = db.SanPhams.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                sanPham = sanPham.Where(p => p.TenSP.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            sanPham = sanPham.OrderBy(p => p.MaSP);
            ViewData["Count"] = sanPham.Count().ToString();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanPham.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPhams/Details/5
        [HasCredential(RoleID = "VIEW_SANPHAM")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        [HasCredential(RoleID = "ADD_SANPHAM")]
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,AnhDaiDien,Gia,KhuyenMai,MoTa,XuatXu,TrongLuong,MaLoai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                //sanPham.AnhDaiDien = "";
                var f = Request.Files["AnhDaiDien"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/assets/Images/SanPham/" + FileName);
                    f.SaveAs(UploadPath);
                    sanPham.AnhDaiDien = FileName;
                }
                else
                {
                    sanPham.AnhDaiDien = "default-img.png";
                }
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        [HasCredential(RoleID = "EDIT_SANPHAM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,AnhDaiDien,Gia,KhuyenMai,MoTa,XuatXu,TrongLuong,MaLoai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                //sanPham.AnhDaiDien = "";
                var f = Request.Files["AnhDaiDien"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/assets/Images/SanPham/" + FileName);
                    f.SaveAs(UploadPath);
                    sanPham.AnhDaiDien = FileName;
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }


        // POST: Admin/SanPhams/Delete/5
        [HasCredential(RoleID = "DELETE_SANPHAM")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
