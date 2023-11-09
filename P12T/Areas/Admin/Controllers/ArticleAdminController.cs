using P12T.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Net;
using System.Web.Mvc;

namespace P12T.Areas.Admin.Controllers
{
    public class ArticleAdminController : Controller
    {
        private P12TEntities db = new P12TEntities();
        public ActionResult Index()
        {
            IEnumerable<Article> objArticleList = db.Articles;
            return View(objArticleList);
        }

        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(Article obj)
        {
            if (ModelState.IsValid)
            {
                obj.CreateAt = DateTime.Now;
                db.Articles.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index", "ArticleAdmin", new { @area = "Admin" });
            }
            return View(obj);
        }

        public ActionResult EditArticle(int id)
        {
            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(Article obj)
        {
            if (ModelState.IsValid)
            {
                obj.CreateAt = DateTime.Now;
                //db.Entry(obj).State = EntityState.Modified;
                db.Articles.AddOrUpdate(obj);
                db.SaveChanges();

                return RedirectToAction("Index", "ArticleAdmin", new { @area = "Admin" });
            }

            return View(obj);
        }

        public ActionResult DeleteArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticleConfirmed(int id)
        {
            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return RedirectToAction("Index", "ArticleAdmin", new { @area = "Admin" });
        }
    }
}