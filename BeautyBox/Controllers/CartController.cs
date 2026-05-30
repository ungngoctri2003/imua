using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBox.Models;
using BeautyBox.Common;

namespace BeautyBox.Controllers
{
    public class CartController : Controller
    {
        private BeautyBoxDB db = new BeautyBoxDB();
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
                return RedirectToAction("Index", "Home", new { auth = "login", returnUrl = Url.Action("Order", "Cart") });
            }

            var cart = Session["cart"] as List<GioHang>;

            if (cart == null || cart.Count == 0)
            {
                if (TempData["OrderId"] != null)
                {
                    ViewBag.OrderId = TempData["OrderId"];
                    ViewBag.OrderTotal = TempData["OrderTotal"];
                    ViewBag.OrderDate = TempData["OrderDate"];
                    ViewBag.ShippingFee = TempData["ShippingFee"];
                    return View();
                }

                return RedirectToAction("Index");
            }

            money = 0;
            foreach (var item in cart)
            {
                money += (item.Product.Gia - (item.Product.Gia * item.Product.KhuyenMai) / 100) * item.Quantity;
            }

            UserLogin user = (UserLogin)Session[CommonConstants.USER_SESSION];
            HoaDon invoid = new HoaDon();
            invoid.MaND = user.UserID;
            invoid.PhiVanChuyen = money >= 400000 ? 0 : 10000;
            invoid.NgayMua = DateTime.Now;
            invoid.ThanhTien = money + invoid.PhiVanChuyen;
            db.HoaDons.Add(invoid);
            db.SaveChanges();

            OrderDetial(invoid);

            Session["cart"] = null;

            TempData["OrderId"] = invoid.MaHD;
            TempData["OrderTotal"] = invoid.ThanhTien;
            TempData["OrderDate"] = invoid.NgayMua;
            TempData["ShippingFee"] = invoid.PhiVanChuyen;

            return RedirectToAction("Order");
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
