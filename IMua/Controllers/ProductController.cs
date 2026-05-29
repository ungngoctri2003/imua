using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMua.Models;

namespace IMua.Controllers
{
    public class ProductController : Controller
    {
        private IMuaDB db = new IMuaDB();
        // GET: Product
        public ActionResult Index(int? MaSP)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            var SanPham = db.SanPhams.Where(sp => sp.MaSP == MaSP).Select(x => x);
            return View(SanPham);
        }
    }
}