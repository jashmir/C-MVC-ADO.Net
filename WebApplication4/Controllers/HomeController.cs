using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        UserInfoDBAccessLayer uidb = new UserInfoDBAccessLayer();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult UserRegister()
        {
            //ViewBag.Message = "Your application description page.";
            return View(new UserInfo());
        }


        [HttpPost]
        public ActionResult UserRegister([Bind] UserInfo ui)
        {
            //ViewBag.Message = "Your application description page.";


            try
            {
                if (ModelState.IsValid)
                {
                    string resp = uidb.AddUserRecord(ui);
                    TempData["msg"] = resp;
                    List<UserInfo> userInfoList = uidb.GetUserRecord();
                    return View("User",userInfoList);
                    //return View("User1", ui);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                //return View(new UserInfo());
            }

            return View(ui);
        }
    }
}