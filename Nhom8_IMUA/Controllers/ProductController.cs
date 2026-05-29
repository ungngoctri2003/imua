using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Controllers
{
    public class ProductController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        // GET: Product
        public ActionResult Index(int? MaSP)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            var SanPham = db.SanPhams.Where(sp => sp.MaSP == MaSP).Select(x => x);
            return View(SanPham);
        }
    }
}