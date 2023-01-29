using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media;
using DAL_Layer.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        ProductDBEntities uidb = new ProductDBEntities();
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
            //var userList = uidb.UserInfoes.ToList();
            return View(new DAL_Layer.Models.UserInfo());
        }


        [HttpPost]
        public ActionResult UserRegister([Bind] DAL_Layer.Models.UserInfo ui, string username, string email, string userpassword)
        {
            //ViewBag.Message = "Your application description page.";


            try
            {
                if (ModelState.IsValid)
                {
                    Console.WriteLine(userpassword);
                    uidb.AddUserInfo(username, email, userpassword);
                    var userList = uidb.UserInfoes.ToList();
                    //string resp = uidb.AddUserRecord(ui);
                    //TempData["msg"] = resp;
                    //List<UserInfo> userInfoList = uidb.GetUserRecord();
                    return View("User",userList);
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