using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P12T.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}