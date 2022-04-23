using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppArticle.Data;

using AppArticle.Models;
using HtmlAgilityPack;
using RabbitMQ.Client;

namespace AppArticle.Controllers
{
    public class SourcesController : Controller
    {
        private Context db = new Context();

        // GET: Sources
        public ActionResult Index()
        {
            return View(db.Sources.ToList());
        }
        public ActionResult linksDetail(string link,string selector)
        {
            var url = link;
            var web = new HtmlWeb();
            var doc = web.Load(url);
            HashSet<String> Links = new HashSet<string>();
            foreach (HtmlNode linkz in doc.DocumentNode.SelectNodes("//div[@class='"+ selector + "']//a[@href]"))
            {
                Links.Add(linkz.GetAttributeValue("href", string.Empty));
            }
            return Json(Links);
        }
        // GET: Sources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // GET: Sources/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Create(Source source)
        {
            if (ModelState.IsValid)
            {
                try{        
                    db.Sources.Add(source);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                  Debug.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,link,titleSelector,descriptionSelector,imgSelector,contentSelector")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Entry(source).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(source);
        }


        // GET: Sources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Source source = db.Sources.Find(id);
            db.Sources.Remove(source);
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
