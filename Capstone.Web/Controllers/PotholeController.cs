using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class PotholeController : Controller
    {
        private const string UsernameKey = "Pothole_UserName";

        public User CurrentUser
        {
            get
            {
                if (IsAuthenticated)
                {
                    return (User)Session["user"];
                }

                return null;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Session["user"] != null;
            }
        }

       /* public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        */

        [ChildActionOnly]
        public ActionResult GetNavbar()
        {
            User model = (User)Session["user"];

            return PartialView("_Navbar", model);
        }

    }
}