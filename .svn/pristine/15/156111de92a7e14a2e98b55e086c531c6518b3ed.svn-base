using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CherryCitySoftware.MedicalOffice.Context;
using CherryCitySoftware.MedicalOffice.Models;
using Microsoft.Owin.Security;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CherryCitySoftware.MedicalOffice.Controllers
{
    public class PatientPoliciesController : Controller
    {
        private ApplicationUserManager _userManager;

        public PatientPoliciesController()
        {
        }

        public PatientPoliciesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PatientPolicies
        public async Task<ActionResult> Index()
        {


            var patientpolicies = db.PatientPolicies.Where(m => m.Patient.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
            return View(await patientpolicies.ToListAsync());
            //          return View(await db.PatientPolicies.ToListAsync());

        }

        // GET: PatientPolicies/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPolicy patientPolicy = await db.PatientPolicies.FindAsync(id);
            if (patientPolicy == null)
            {
                return HttpNotFound();
            }
            return View(patientPolicy);
        }

        // GET: PatientPolicies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientPolicies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Policy")] PatientPolicy patientPolicy, FormCollection form, string PatientId)
        {
            if (ModelState.IsValid)
            {

                var patient = form.GetValue("Patient.Id").AttemptedValue.ToString();
                patientPolicy.Patient = await db.Users.FirstAsync(u => u.Id.Equals(patient, StringComparison.InvariantCultureIgnoreCase));

                db.PatientPolicies.Add(patientPolicy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = patient });


            }

            return View(patientPolicy);
        }
        public async Task<ActionResult> CreatePolicy(string PatientId)
        {
            var patient = await db.Users.FirstAsync(u => u.Id.Equals(PatientId, StringComparison.InvariantCultureIgnoreCase));

            return View(new PatientPolicy() { Patient = patient });


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePolicy([Bind(Include = "Id,Policy")] PatientPolicy patientPolicy, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var patient = db.Users.Find(form["Patient.Id"]);
                patientPolicy.Patient = patient;
                db.PatientPolicies.Add(patientPolicy);
                await db.SaveChangesAsync();
                
                await UserManager.SendEmailAsync(patient.Id, "You have new policy", " Please login in and check ");
                return RedirectToAction("UserDetails", "Account", new { id = form["Patient.Id"] });


            }

            return View(patientPolicy);
        }
        // GET: PatientPolicies/Edit/5
        public async Task<ActionResult> Edit(long? id, string PatientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPolicy patientPolicy = await db.PatientPolicies.FindAsync(id);
            var patient = db.Users.Find(PatientId);
            patientPolicy.Patient = patient;
            if (patientPolicy == null)
            {
                return HttpNotFound();
            }
            return View(patientPolicy);
        }

        // POST: PatientPolicies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Policy")] PatientPolicy patientPolicy, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var patient = form.GetValue("Patient.Id").AttemptedValue.ToString();
                patientPolicy.Patient = await db.Users.FirstAsync(u => u.Id.Equals(patient, StringComparison.InvariantCultureIgnoreCase));
                db.Entry(patientPolicy).State = EntityState.Modified;
                await db.SaveChangesAsync();
               
                await UserManager.SendEmailAsync(patientPolicy.Patient.Id, "Your policies have been changed", " Please login in and check ");
                return RedirectToAction("UserDetails", "Account", new { id = form["Patient.Id"] });
                //             return RedirectToAction("Index");
            }
            return View(patientPolicy);
        }

        // GET: PatientPolicies/Delete/5
        public async Task<ActionResult> Delete(long? id, string PatientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPolicy patientPolicy = await db.PatientPolicies.FindAsync(id);
            var patient = db.Users.Find(PatientId);
            patientPolicy.Patient = patient;
            if (patientPolicy == null)
            {
                return HttpNotFound();
            }
            return View(patientPolicy);
        }

        // POST: PatientPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id, string PatientId)
        {
            PatientPolicy patientPolicy = await db.PatientPolicies.FindAsync(id);

            db.PatientPolicies.Remove(patientPolicy);
            await db.SaveChangesAsync();
            return RedirectToAction("UserDetails", "Account", new { id = PatientId });
            //         return RedirectToAction("Index");
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
