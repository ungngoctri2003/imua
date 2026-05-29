using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMua.Models;
using IMua.Common;

namespace IMua.Controllers
{
    public class UserController : Controller
    {
        IMuaDB db = new IMuaDB();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("Index", "Home", new { auth = "register" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                model.AnhDaiDien = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/assets/Images/AnhDaiDien/" + FileName);
                    f.SaveAs(UploadPath);
                    model.AnhDaiDien = FileName;
                }
                else
                {
                    model.AnhDaiDien = "default-avatar.png";
                }
                var dao = new UserDAO();
                if (dao.CheckUserName(model.TenDangNhap))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                    ViewBag.Error = true;
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new NguoiDung();
                    user.TenDangNhap = model.TenDangNhap;
                    user.MatKhau = Encryptor.MD5Hash(model.MatKhau);
                    user.HoTen = model.HoTen;
                    user.SoDT = model.SoDT;
                    user.Email = model.Email;
                    user.DiaChi = model.DiaChi;
                    user.AnhDaiDien = model.AnhDaiDien;
                    user.TrangThai = true;
                    user.GroupID = "MEMBER";
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ModelState.Clear();
                        if (Request.IsAjaxRequest())
                        {
                            return Json(new { success = true, message = "Đăng ký thành công! Vui lòng đăng nhập." });
                        }
                        return RedirectToAction("Index", "Home", new { auth = "login" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại");
                    }

                }
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = false,
                    message = GetModelErrorMessage()
                });
            }
            return RedirectToAction("Index", "Home", new { auth = "register" });
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return RedirectToAction("Index", "Home", new { auth = "login", returnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.TenDangNhap, Encryptor.MD5Hash(model.MatKhau));
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị vô hiệu hóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                else
                {
                    var user = dao.GetByID(model.TenDangNhap);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TenDangNhap;
                    userSession.Password = user.MatKhau;
                    userSession.UserID = user.MaND;
                    userSession.HoTen = user.HoTen;
                    userSession.AnhDaiDien = user.AnhDaiDien;
                    userSession.GroupID = user.GroupID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    if (result == 1)
                    {
                        var redirect = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
                        if (Request.IsAjaxRequest())
                        {
                            return Json(new { success = true, message = "Đăng nhập thành công!", redirectUrl = redirect });
                        }
                        return Redirect(redirect);
                    }
                    else
                    {
                        var listCredentials = dao.GetListCredential(model.TenDangNhap);
                        Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                        if (Request.IsAjaxRequest())
                        {
                            return Json(new { success = true, message = "Đăng nhập thành công!", redirectUrl = "/Admin/TrangChu/Index" });
                        }
                        return Redirect("/Admin/TrangChu/Index");
                    }
                }
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { success = false, message = GetModelErrorMessage() });
            }
            return RedirectToAction("Index", "Home", new { auth = "login", returnUrl = returnUrl });
        }

        private string GetModelErrorMessage()
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)
                .Where(m => !string.IsNullOrEmpty(m));
            return string.Join(" ", errors);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session[CommonConstants.SESSION_CREDENTIALS] = null;
            Session["Cart"] = null;
            return Redirect("/");
        }

        public ActionResult ChangeInfor(int id)
        {
            var user = new UserDAO().ViewDetails(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfor(NguoiDung nguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nguoiDung.AnhDaiDien = "";

                    var backupImg = Request["Image1"];
                    var f = Request.Files["ImageFile"];

                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/assets/Images/AnhDaiDien/" + FileName);
                        f.SaveAs(UploadPath);
                        nguoiDung.AnhDaiDien = FileName;
                    }
                    else
                    {
                        nguoiDung.AnhDaiDien = backupImg;
                    }
                    var userSession = new UserLogin();
                    var user = db.NguoiDungs.Find(nguoiDung.MaND);
                    user.HoTen = nguoiDung.HoTen;
                    user.SoDT = nguoiDung.SoDT;
                    user.Email = nguoiDung.Email;
                    user.DiaChi = nguoiDung.DiaChi;
                    user.AnhDaiDien = nguoiDung.AnhDaiDien;
                    db.SaveChanges();
                    userSession.UserID = user.MaND;
                    userSession.HoTen = user.HoTen;
                    userSession.AnhDaiDien = user.AnhDaiDien;
                    userSession.GroupID = user.GroupID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    ViewBag.Success = "Thay đổi thông tin thành công";
                }
                else
                {
                    var backupImg = Request["Image1"];
                    nguoiDung.AnhDaiDien = backupImg;
                    //ModelState.AddModelError("", "Thay đổi thông tin thất bại");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error(ex.Message);
            }
            return View(nguoiDung);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var session = (IMua.Common.UserLogin)Session[IMua.Common.CommonConstants.USER_SESSION];
                    if (session != null)
                    {
                        if (session.Password != Encryptor.MD5Hash(oldpass))
                        {
                            ModelState.AddModelError("ErrorUpdate", "Mật khẩu cũ không chính xác");
                        }
                        else
                        {
                            NguoiDung edit = db.NguoiDungs.Where(p => p.MaND == session.UserID).FirstOrDefault();
                            edit.MatKhau = Encryptor.MD5Hash(newpass);
                            db.SaveChanges();
                            session.Password = Encryptor.MD5Hash(newpass);
                            ViewBag.Success = "Thay đổi mật khẩu thành công";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("ErrorUpdate", "Đổi mật khẩu thất bại");
                }
            } catch (Exception ex)
            {
                ViewBag.Error(ex.Message);
            }
            return View();
        }
    }
}