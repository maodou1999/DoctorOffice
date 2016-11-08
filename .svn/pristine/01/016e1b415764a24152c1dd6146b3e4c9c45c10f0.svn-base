using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CherryCitySoftware.MedicalOffice.Context;
using CherryCitySoftware.MedicalOffice.Models;

namespace CherryCitySoftware.MedicalOffice.Controllers
{
    public class ResourceLinksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResourceLinks
        public ActionResult Index()
        {
            return View(db.ResourceLinks.ToList());
        }

        // GET: ResourceLinks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceLink resourceLink = db.ResourceLinks.Find(id);
            if (resourceLink == null)
            {
                return HttpNotFound();
            }
            return View(resourceLink);
        }

        // GET: ResourceLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ResourceLinkName,ResourceLinkAddress")] ResourceLink resourceLink)
        {
            if (ModelState.IsValid)
            {
                db.ResourceLinks.Add(resourceLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resourceLink);
        }

        // GET: ResourceLinks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceLink resourceLink = db.ResourceLinks.Find(id);
            if (resourceLink == null)
            {
                return HttpNotFound();
            }
            return View(resourceLink);
        }

        // POST: ResourceLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResourceLinkName,ResourceLinkAddress")] ResourceLink resourceLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resourceLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resourceLink);
        }

        // GET: ResourceLinks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceLink resourceLink = db.ResourceLinks.Find(id);
            if (resourceLink == null)
            {
                return HttpNotFound();
            }
            return View(resourceLink);
        }

        // POST: ResourceLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ResourceLink resourceLink = db.ResourceLinks.Find(id);
            db.ResourceLinks.Remove(resourceLink);
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
