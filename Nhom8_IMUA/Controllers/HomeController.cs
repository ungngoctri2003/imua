using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Controllers
{
    public class HomeController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        //private Nhom8Entities topsell = new Nhom8Entities();
        public ActionResult Index()
        {
            //San pham ban chay
            //ViewBag.SanPhamBanChay = db.SanPhams.OrderBy(p=>p.MaSP).Select(p => p).Take(5);
            ViewBag.SanPhamBanChay = db.SanPhamBanChays.Take(5);

            //Danh muc san pham
            ViewBag.DanhMuc = db.DanhMucs.Include("LoaiSPs").Select(p => p).Take(4);

            //Tin tuc
            ViewBag.TinTuc = db.TinTucs.Select(p => p).Take(4);

            List<GioHang> cart = (List<GioHang>)Session["cart"];

            return View();
        }

        [ChildActionOnly]
        public ActionResult DanhMuc()
        {
            var danhMuc = db.DanhMucs.Include("LoaiSPs").Select(p => p).Take(8);
            return PartialView(danhMuc);
        }

        [ChildActionOnly]
        public ActionResult Product(int? MaDM)
        {
            var sanPham = db.SanPhams.Where(sp => sp.LoaiSP.DanhMuc.MaDM == MaDM).Select(x => x);
            return PartialView(sanPham);
        }

        public ActionResult ShoppingGuide()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult ProductSaleOff()
        {
            var sanPham = db.SanPhams.Select(x => x);
            return PartialView(sanPham);
        }

        [ChildActionOnly]
        public ActionResult CartCount()
        {
            List<GioHang> cart = (List<GioHang>)Session["cart"];
            return PartialView(cart);
        }

        public ActionResult ListProductSaleOff(int? page)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            var sp = db.SanPhams.Where(p => p.KhuyenMai != 0).Select(p => p);
            sp = sp.OrderBy(s => s.MaSP);
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return PartialView(sp.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductType(int? id)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            var loaiSP = db.LoaiSPs.Where(p => p.MaLoai == id).Select(p => p);
            ViewBag.SanPham = db.SanPhams.Where(p => p.MaLoai == id).Select(p => p);
            return PartialView(loaiSP);
        }

        public ActionResult ListNews(int? page)
        {
            var news = db.TinTucs.Select(p => p);
            news = news.OrderBy(s => s.MaTinTuc);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NewsDetail(int id)
        {
            var newsDetail = db.TinTucs.Where(s => s.MaTinTuc == id).Select(p => p);
            return PartialView("NewsDetail", newsDetail);
        }

        public ActionResult Contact()
        {
            return PartialView();
        }
    }
}