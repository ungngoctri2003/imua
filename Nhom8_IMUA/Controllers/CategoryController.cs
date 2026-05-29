using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Controllers
{
    public class CategoryController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        public ActionResult Index(int? id)
        {
            var dm = db.DanhMucs.Where(p => p.MaDM == id).ToList();
            ViewBag.LoaiSP = db.LoaiSPs.Include("SanPhams").Where(p=>p.DanhMuc.MaDM == id).Select(p => p);
            //ViewBag.SanPham = db.SanPhams.Where(p => p.LoaiSP.DanhMuc.MaDM == id).Select(p => p).Take(8);
            List<GioHang> li = (List<GioHang>)Session["cart"];
            return View("Index", dm);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var danhMuc = db.DanhMucs.Select(x => x);
            return PartialView("Menu",danhMuc);
        }

        [ChildActionOnly]
        public ActionResult Product(int? id)
        {
            var sp = db.SanPhams.Where(p => p.LoaiSP.MaLoai == id).Select(p => p).Take(8);
            return PartialView("Product", sp);
        }
    }
}