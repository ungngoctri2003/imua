using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBox.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string HoTen { set; get; }
        public string AnhDaiDien { set; get; }
        public string GroupID { set; get; }
    }
}