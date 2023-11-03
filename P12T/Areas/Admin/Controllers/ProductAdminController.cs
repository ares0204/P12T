using P12T.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P12T.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        private P12TEntities db = new P12TEntities();

        //GET
        public ActionResult AddProduct()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product obj)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index", "HomeAdmin", new { @area = "Admin"});
            }
            return View(obj);
        }

        //GET
        public ActionResult EditProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var productsFromDb = db.Products.Find(id);

            if (productsFromDb == null)
            {
                return HttpNotFound();
            }

            return View(productsFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product obj)
        {
            if (ModelState.IsValid)
            {
                db.Products.AddOrUpdate(obj);
                db.SaveChanges();
                return RedirectToAction("Index", "HomeAdmin", new { @area = "Admin" });
            }
            return View(obj);
        }

        //GET
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var productsFromDb = db.Products.Find(id);

            if (productsFromDb == null)
            {
                return HttpNotFound();
            }

            return View(productsFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int id)
        {
            var obj = db.Products.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", "HomeAdmin", new { @area = "Admin" });
        }
    }
}