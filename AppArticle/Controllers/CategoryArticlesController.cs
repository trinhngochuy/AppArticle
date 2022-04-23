using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppArticle.Data;
using AppArticle.Models;

namespace AppArticle.Controllers
{
    public class CategoryArticlesController : Controller
    {
        private Context db = new Context();

        // GET: CategoryArticles
        public ActionResult Index()
        {
            return View(db.CategoryArticles.ToList());
        }

        // GET: CategoryArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryArticle categoryArticle = db.CategoryArticles.Find(id);
            if (categoryArticle == null)
            {
                return HttpNotFound();
            }
            return View(categoryArticle);
        }

        // GET: CategoryArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] CategoryArticle categoryArticle)
        {
            if (ModelState.IsValid)
            {
                db.CategoryArticles.Add(categoryArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryArticle);
        }

        // GET: CategoryArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryArticle categoryArticle = db.CategoryArticles.Find(id);
            if (categoryArticle == null)
            {
                return HttpNotFound();
            }
            return View(categoryArticle);
        }

        // POST: CategoryArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] CategoryArticle categoryArticle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryArticle);
        }

        // GET: CategoryArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryArticle categoryArticle = db.CategoryArticles.Find(id);
            if (categoryArticle == null)
            {
                return HttpNotFound();
            }
            return View(categoryArticle);
        }

        // POST: CategoryArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryArticle categoryArticle = db.CategoryArticles.Find(id);
            db.CategoryArticles.Remove(categoryArticle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
