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
using Microsoft.Reporting.WebForms;
using Nhom8_IMUA.Common;

namespace Nhom8_IMUA.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/HoaDons
        [HasCredential(RoleID = "VIEW_HOADON")]
        public ActionResult Index(int? page)
        {
            var hoaDon = db.HoaDons.Select(p => p).OrderBy(s => s.MaHD);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hoaDon.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HoaDons/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var hoaDon = db.HoaDons.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                hoaDon = hoaDon.Where(p => p.NguoiDung.HoTen.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            hoaDon = hoaDon.OrderBy(p => p.MaHD);
            ViewData["Count"] = hoaDon.Count().ToString();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hoaDon.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HoaDons/Details/5
        [HasCredential(RoleID = "VIEW_HOADON")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        [HasCredential(RoleID = "ADD_HOADON")]
        public ActionResult Create()
        {
            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "TenDangNhap");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,PhiVanChuyen,ThanhTien,NgayMua,MaND")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "TenDangNhap", hoaDon.MaND);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        [HasCredential(RoleID = "EDIT_HOADON")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "TenDangNhap", hoaDon.MaND);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,PhiVanChuyen,ThanhTien,NgayMua,MaND")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "TenDangNhap", hoaDon.MaND);
            return View(hoaDon);
        }


        // POST: Admin/HoaDons/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return Json(new { ThongBao = "successs" });
        }

        public ActionResult ExportFile(int? maHD, int? maND, string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Areas/Admin/Reports/InvoiceReport.rdlc");

            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "HoaDon";
            reportDataSource1.Value = db.HoaDons.Where(p => p.MaHD == maHD).ToList();
            localreport.DataSources.Add(reportDataSource1);

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "ChiTietHoaDon";
            reportDataSource2.Value = db.ChiTietHoaDons.Where(p => p.MaHD == maHD).Include(p => p.SanPham).ToList();
            localreport.DataSources.Add(reportDataSource2);

            ReportDataSource reportDataSource3 = new ReportDataSource();
            reportDataSource3.Name = "NguoiDung";
            reportDataSource3.Value = db.NguoiDungs.Where(p => p.MaND == maND).ToList();
            localreport.DataSources.Add(reportDataSource3);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else if (reportType == "Image")
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename= HoaDon." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
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
