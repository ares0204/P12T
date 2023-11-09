using Microsoft.Ajax.Utilities;
using P12T.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        public ActionResult News()
        {
            IEnumerable<Article> objArticleList = db.Articles;

            return View(objArticleList);
        }

        public ActionResult DetailProduct(int id)
        {
            Product obj = db.Products.FirstOrDefault(p => p.ProductId == id);

            if (obj != null)
            {
                return View(obj);
            }
            else
            {
                return View("ProductNotFound");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(Order obj, int quantity)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = db.Orders.FirstOrDefault(o => o.ProductId == obj.ProductId);

                if (existingOrder == null)
                {
                    obj.Quantity = quantity; // Set the quantity to the entered value
                    db.Orders.Add(obj);
                }
                else
                {
                    Order orderItem = db.Orders.FirstOrDefault(m => m.ProductId == obj.ProductId);
                    orderItem.Quantity += quantity; // Update the quantity with the entered value
                }

                db.SaveChanges();
                return RedirectToAction("Cart");
            }

            return View(obj);
        }

        public ActionResult Cart()
        {
            IEnumerable<Cart> objCartList = db.Carts;

            return View(objCartList);
        }
    }
}