using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Controllers
{
    public class TimKiemController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        // GET: TimKiem
        public ActionResult Index(string searchString, int? page)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            ViewBag.CurrentFilter = searchString;

            var sanphams = db.SanPhams.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                sanphams = sanphams.Where(p => p.TenSP.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            sanphams = sanphams.OrderBy(s => s.MaSP);
            ViewData["Count"] = sanphams.Count().ToString();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanphams.ToPagedList(pageNumber, pageSize));
        }
    }
}