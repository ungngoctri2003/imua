using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMua.Models;

namespace IMua.Controllers
{
    public class TimKiemController : Controller
    {
        private IMuaDB db = new IMuaDB();
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