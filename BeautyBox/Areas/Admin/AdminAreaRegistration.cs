using System.Web.Mvc;

namespace BeautyBox.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_thongke",
                "Admin/ThongKe",
                new { controller = "ThongKe", action = "Index" },
                namespaces: new[] { "BeautyBox.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BeautyBox.Areas.Admin.Controllers" }
            );
        }
    }
}