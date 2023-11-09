using P12T.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P12T.Controllers
{
    public class CartController : Controller
    {
        private P12TEntities db = new P12TEntities();

        public ActionResult RemoveFromCart(int id)
        {
            Order itemToRemove = db.Orders.FirstOrDefault(order => order.ProductId == id);

            if (itemToRemove != null)
            {
                db.Orders.Remove(itemToRemove);
                db.SaveChanges();
            }

            return RedirectToAction("Cart", "Home");
        }

        public ActionResult UpdateCart(int id, int quantity)
        {
            Order item = db.Orders.FirstOrDefault(order => order.ProductId == id);

            if (item != null)
            {
                item.Quantity = quantity;
                db.Orders.AddOrUpdate(item);
                db.SaveChanges();
            }

            return RedirectToAction("Cart", "Home");
        }

        [HttpPost]
        public ActionResult UpdateCartTest(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                Order itemToRemove = db.Orders.FirstOrDefault(order => order.ProductId == productId);
                db.Orders.Remove(itemToRemove);
                db.SaveChanges();
                return RedirectToAction("Cart", "Home");
            }

            Order item = db.Orders.FirstOrDefault(order => order.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;

                try
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Handle any database update errors (e.g., show an error message).
                    // You can log the exception for debugging purposes.
                    // For example: Log.Error(ex, "Database update error");
                }
            }

            return RedirectToAction("Cart", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReport(Report obj, double money)
        {
            obj.Money = money;
            obj.CreatedAt = DateTime.Now;
            db.Reports.Add(obj);
            db.SaveChanges();

            db.Database.ExecuteSqlCommand("DELETE FROM [Orders]");

            return RedirectToAction("Cart", "Home");
        }
    }
}