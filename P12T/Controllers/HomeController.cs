using P12T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P12T.Controllers
{
    public class HomeController : Controller
    {

        private P12TEntities db = new P12TEntities();

        public ActionResult Index()
        {
            IEnumerable<Product> objProductList = db.Products;
            return View(objProductList);
        }
    }
}