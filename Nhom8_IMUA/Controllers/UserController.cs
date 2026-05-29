using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using Nhom8_IMUA.Models;
using Nhom8_IMUA.Common;

namespace Nhom8_IMUA.Controllers
{
    public class UserController : Controller
    {
        Nhom8DB db = new Nhom8DB();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")]
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
                        ViewBag.Success = "Đăng ký thành công";
                        ModelState.Clear();
                        MvcCaptcha.ResetCaptcha("registerCaptcha");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại");
                    }

                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
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
                        return Redirect("/");
                    }
                    else
                    {
                        var listCredentials = dao.GetListCredential(model.TenDangNhap);
                        Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                        return Redirect("/Admin/TrangChu/Index");
                    }
                }
            }

            return View();
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
                    var session = (Nhom8_IMUA.Common.UserLogin)Session[Nhom8_IMUA.Common.CommonConstants.USER_SESSION];
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