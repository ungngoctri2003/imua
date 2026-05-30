using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBox.Models;

namespace BeautyBox.Controllers
{
    public class ProductController : Controller
    {
        private BeautyBoxDB db = new BeautyBoxDB();
        // GET: Product
        public ActionResult Index(int? MaSP)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            var SanPham = db.SanPhams.Where(sp => sp.MaSP == MaSP).Select(x => x);
            return View(SanPham);
        }
    }
}