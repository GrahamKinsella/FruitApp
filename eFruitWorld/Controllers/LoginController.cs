using eFruitWorld.Context;
using eFruitWorld.Models;
using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace eFruitWorld.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(LoginModel Model)
        {
            User user = new User
            {
                Username = Model.Username,
                Password = Model.Password
            };

            if (ModelState.IsValid)
            {
                using (CartContext db = new CartContext())
                {
                    var obj = db.User.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.Username.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "User does not exist or Password is incorrect";
                    }
                }
            }

            return View("Login");
        }


        public ActionResult Register()
        {
            return View("Register");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CartContext())
                {
                    var res = db.User.Where(u => u.Username == Model.Username).Select(a => a).ToList();
                    if (res.Count == 0)
                    {
                        var User = new User()
                        {
                            Username = Model.Username,
                            Password = Model.Password,
                        };
                        var order = db.User.Add(User);
                        db.SaveChanges();
                        ViewBag.Message = "User Registered Successfully";
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.Message = "User Already Registered";
                        return View("Login");
                    }
                }
            }
            return View("Register");
        }
    }
}