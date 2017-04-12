using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : PotholeController
    {
        private IPotholeDAL potholeDAL;

        public HomeController(IPotholeDAL potholeDAL)
        {
            this.potholeDAL = potholeDAL;
        }
       
        public ActionResult Index()
        {
            List<PotholeModel> model = potholeDAL.GetAllPotholes();
            return View("Index", model);
        }
    }
}