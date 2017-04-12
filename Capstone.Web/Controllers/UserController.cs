using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class UserController : PotholeController
    {
        private IUserDAL userDAL;

        public UserController(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("user/register")]
        public ActionResult Register()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var model = new RegisterModel();
                return View("Register", model);
            }
        }

        [HttpPost]
        [Route("user/register")]
        public ActionResult Register(RegisterModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", newUser);
            }
            else if(userDAL.VerifyUniqueEmail(newUser.Email) == false)
            {
                ModelState.AddModelError("existing-user", "An account with this email address already exists");
                return View("Register", newUser);
            }

            userDAL.RegisterUser(newUser);

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Route("user/login")]
        public ActionResult Login()
        {
            if (Session["user"] != null)//if user is already logged in, this won't allow to them to log in again
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Login");
        }

        [HttpPost]
        [Route("user/login")]
        public ActionResult Login(LoginModel loginModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Login", loginModel);
            }

            int loggedInUserId = userDAL.LogInUser(loginModel);

            if(loggedInUserId == -1)//loggedInUser = -1 if email or password is not valid
            {
                ModelState.AddModelError("invalid-login", "The email or password combination is not valid");
                return View("Login", loginModel);
            }

            User loggedInUser = userDAL.GetUser(loggedInUserId);

            Session["userId"] = loggedInUserId;
            Session["user"] = loggedInUser;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            return RedirectToAction("Index", "Home");
        }

    }
}