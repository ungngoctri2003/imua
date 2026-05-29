using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhom8_IMUA.Models;
using Nhom8_IMUA.Common;

namespace Nhom8_IMUA.Models
{
    public class UserDAO
    {
        Nhom8DB db = null;
        public UserDAO()
        {
            db = new Nhom8DB();
        }

        public long Insert(NguoiDung entity)
        {
            db.NguoiDungs.Add(entity);
            db.SaveChanges();
            return entity.MaND;
        }

        public bool CheckUserName(string userName)
        {
            return db.NguoiDungs.Count(x => x.TenDangNhap == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.NguoiDungs.Count(x => x.Email == email) > 0;
        }

        public NguoiDung GetByID(string userName)
        {
            return db.NguoiDungs.SingleOrDefault(p => p.TenDangNhap == userName);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.NguoiDungs.SingleOrDefault(p => p.TenDangNhap == userName);
            if (result == null)
            {
                //Tài khoản không tồn tại
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    //Tài khoản đã bị vô hiệu hóa
                    return -1;

                }
                else
                {
                    if (result.MatKhau == passWord)
                    {
                        if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                        {
                            //admin and mod
                            return 2;
                        }
                        else
                        {
                            //member
                            return 1;
                        }
                    }
                    else
                    {
                        //Sai mật khẩu
                        return -2;
                    }
                }
            }
        }

        public NguoiDung ViewDetails(int id)
        {
            return db.NguoiDungs.Find(id);
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.NguoiDungs.Single(x => x.TenDangNhap == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.GroupID equals b.GroupID
                        join c in db.Roles on a.RoleID equals c.RoleID
                        where b.GroupID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            GroupID = a.GroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            GroupID = x.GroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
    }
}