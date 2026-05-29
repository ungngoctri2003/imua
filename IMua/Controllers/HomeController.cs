using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMua.Models;

namespace IMua.Controllers
{
    public class HomeController : Controller
    {
        private IMuaDB db = new IMuaDB();
        //private IMuaEntities topsell = new IMuaEntities();
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
            var loaiSP = db.LoaiSPs.Where(p => p.MaLoai == id).ToList();
            ViewBag.SanPham = db.SanPhams.Where(p => p.MaLoai == id).ToList();
            return View(loaiSP);
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
            var article = db.TinTucs.FirstOrDefault(t => t.MaTinTuc == id);
            if (article == null)
            {
                return View(Enumerable.Empty<TinTuc>());
            }

            ViewBag.RelatedNews = db.TinTucs
                .Where(t => t.MaTinTuc != id)
                .OrderByDescending(t => t.MaTinTuc)
                .Take(3)
                .ToList();

            return View(article);
        }

        public ActionResult Contact()
        {
            return PartialView();
        }
    }
}