using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Nhom8_IMUA.Common;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Areas.Admin.Controllers
{
    public class NguoiDungsController : Controller
    {
        private Nhom8DB db = new Nhom8DB();

        // GET: Admin/NguoiDungs
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(int? page)
        {
            var nguoiDung = db.NguoiDungs.Select(p => p).OrderBy(s => s.MaND);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(nguoiDung.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NguoiDungs/Search
        public ActionResult Search(string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            var nguoiDung = db.NguoiDungs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                nguoiDung = nguoiDung.Where(p => p.HoTen.Trim().Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            nguoiDung = nguoiDung.OrderBy(p => p.MaND);
            ViewData["Count"] = nguoiDung.Count().ToString();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(nguoiDung.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NguoiDungs/ChangeStatus
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult ChangeStatus(int? id)
        {
            Response.Write("<script>alert(" + id + ");</script>");
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            NguoiDung custom = db.NguoiDungs.Find(id);
            if (custom.TrangThai.Equals(false))
            {
                custom.TrangThai = true;
            }
            else
            {
                custom.TrangThai = false;
            }
            db.Entry(custom).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/NguoiDungs/Details/5
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.UserGroups, "GroupID", "Name");
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaND,TenDangNhap,MatKhau,HoTen,AnhDaiDien,SoDT,DiaChi,Email,Loai,TrangThai,GroupID")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.UserGroups, "GroupID", "Name", nguoiDung.GroupID);
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.UserGroups, "GroupID", "Name", nguoiDung.GroupID);
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaND,TenDangNhap,MatKhau,HoTen,AnhDaiDien,SoDT,DiaChi,Email,Loai,TrangThai,GroupID")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.UserGroups, "GroupID", "Name", nguoiDung.GroupID);
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NguoiDung nguoiDung = db.NguoiDungs.Find(id);
        //    if (nguoiDung == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nguoiDung);
        //}

        // POST: Admin/NguoiDungs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NguoiDung nguoiDung = db.NguoiDungs.Find(id);
        //    db.NguoiDungs.Remove(nguoiDung);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session[CommonConstants.SESSION_CREDENTIALS] = null;
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
            if (ModelState.IsValid)
            {
                var session = (Nhom8_IMUA.Common.UserLogin)Session[Nhom8_IMUA.Common.CommonConstants.USER_SESSION];
                if (session != null)
                {
                    if (session.Password != Encryptor.MD5Hash(oldpass))
                    {
                        ModelState.AddModelError("", "Mật khẩu cũ không chính xác");
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
                ModelState.AddModelError("", "Đổi mật khẩu thất bại");
            }
            return View();
        }
    }
}
