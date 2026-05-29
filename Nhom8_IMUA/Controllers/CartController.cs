using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom8_IMUA.Models;
using Nhom8_IMUA.Common;

namespace Nhom8_IMUA.Controllers
{
    public class CartController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        private decimal money;
        // GET: Cart
        public ActionResult Index()
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            return View(li);
        }

        public JsonResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<GioHang> cart = new List<GioHang>();
                cart.Add(new GioHang() { Product = db.SanPhams.Find(id), Quantity = quantity });
                Session["cart"] = cart;
            }
            else
            {
                List<GioHang> cart = (List<GioHang>)Session["cart"];
                //neu sp ton tai
                if (cart.Exists(x => x.Product.MaSP == id))
                {
                    foreach (var item in cart)
                    {
                        if (item.Product.MaSP == id)
                            item.Quantity += quantity;
                    }
                }
                else
                {
                    cart.Add(new GioHang() { Product = db.SanPhams.Find(id), Quantity = quantity });
                }
                Session["cart"] = cart;
            }
            return Json(new { SoLuong = ((List<GioHang>)Session["cart"]).Count });
        }

        public JsonResult UpdateCart(int id, int quantity)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            if (li.Exists(x => x.Product.MaSP == id))
            {
                foreach (var item in li)
                {
                    if (item.Product.MaSP == id)
                    {
                        item.Quantity = quantity;
                    }
                }
            }
            Session["cart"] = li;
            return Json(new { QuantitySL = quantity });
        }

        public JsonResult RemoveCart(int id)
        {
            List<GioHang> li = (List<GioHang>)Session["cart"];
            li.RemoveAll(x => x.Product.MaSP == id);
            Session["cart"] = li;
            return Json(new { Ma = id });
        }

        public ActionResult Order()
        {
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                List<GioHang> li = (List<GioHang>)Session["cart"];
                foreach (var item in li)
                {
                    money += (item.Product.Gia - (item.Product.Gia * item.Product.KhuyenMai) / 100) * item.Quantity;
                }
                UserLogin user = (UserLogin)Session[CommonConstants.USER_SESSION];
                HoaDon invoid = new HoaDon();
                invoid.MaND = user.UserID;
                if (money >= 400000)
                {
                    invoid.PhiVanChuyen = 0;
                }
                else
                {
                    invoid.PhiVanChuyen = 10000;
                }
                invoid.NgayMua = System.DateTime.Now;
                invoid.ThanhTien = decimal.Parse(money + "") + invoid.PhiVanChuyen;
                db.HoaDons.Add(invoid);
                db.SaveChanges();
                //Thêm chi tiết hóa đơn
                OrderDetial(invoid);
                Session["Cart"] = null;
            }
            return View();
        }

        public void OrderDetial(HoaDon invoid)
        {

            List<GioHang> cart = (List<GioHang>)Session["cart"];
            foreach (var item in cart)
            {
                ChiTietHoaDon detailInvoid = new ChiTietHoaDon();
                detailInvoid.MaHD = invoid.MaHD;
                detailInvoid.MaSP = item.Product.MaSP;
                detailInvoid.SoLuong = item.Quantity;

                db.ChiTietHoaDons.Add(detailInvoid);
                db.SaveChanges();
            }
        }
    }
}
