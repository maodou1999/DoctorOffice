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
    [Authorize(Roles = "Admin")]
    public class LoggingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Logging
        public ActionResult Index()
        {
            return View(db.LoggingMessages.OrderByDescending(c=>c.ID).Take(100).ToList());
        }

        // GET: Logging/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoggingMessage loggingMessage = db.LoggingMessages.Find(id);
            if (loggingMessage == null)
            {
                return HttpNotFound();
            }
            return View(loggingMessage);
        }

      

     
        // GET: Logging/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoggingMessage loggingMessage = db.LoggingMessages.Find(id.Value);
            db.LoggingMessages.Remove(loggingMessage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Logging/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LoggingMessage loggingMessage = db.LoggingMessages.Find(id);
            db.LoggingMessages.Remove(loggingMessage);
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
