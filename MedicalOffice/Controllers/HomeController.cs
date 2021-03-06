﻿using CherryCitySoftware.MedicalOffice.Context;
using CherryCitySoftware.MedicalOffice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CherryCitySoftware.MedicalOffice.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [RequireSsl]
        public ActionResult Index()
        {
            var model = new HomeViewModel() 
            {
                Documents = db.DocFiles.Where(f => f.Message == null).OrderBy(d => d.FileName).ToList(),
                Contact = GetContactInfo()
            };
            return View(model);
        }

 //       [Authorize]
        
        public ActionResult OfficeHour()
        {
            //OfficeHour regularhour = db.OfficeHours.Find(1); ;
            //OfficeHour currentweekhour = db.OfficeHours.Find(2);
            var hours = db.OfficeHours.OrderBy(h => h.Id).ToList();
            var holidays = db.HolidayNotes.OrderBy(h => h.Id).ToList();

            if (!hours.Any())
            {
                db.OfficeHours.Add(new OfficeHour()
                {
                    Monday = "8:30AM - 5:00PM",
                    Tuesday = "8:30AM - 5:00PM",
                    Wednesday = "8:30AM - 5:00PM",
                    Thursday = "8:30AM - 5:00PM",
                    Friday = "8:30AM - 4:30PM",
                    Saturday = "9:30AM - 3:30PM",
                    Sunday = "Closed"
                });
                db.OfficeHours.Add(new OfficeHour()
                {
                    Monday = "8:30AM - 5:00PM",
                    Tuesday = "8:30AM - 5:00PM",
                    Wednesday = "8:30AM - 5:00PM",
                    Thursday = "8:30AM - 5:00PM",
                    Friday = "8:30AM - 4:30PM",
                    Saturday = "9:30AM - 3:30PM",
                    Sunday = "Closed"
                });

                db.SaveChanges();

                hours = db.OfficeHours.ToList();

            }

            if (!holidays.Any())
            {
                db.HolidayNotes.Add(new HolidayNote()
                {
                    Holidays = "Thanksgiving "
                });
               
                db.SaveChanges();

                holidays = db.HolidayNotes.ToList();
            }
            //HolidayEntity  holiday=db.Holiday.Find(1);
            OfficeHourViewModel OfficeHourView = new OfficeHourViewModel { Regualhour = hours[0], Currenthour = hours[1], Holiday = holidays[0] };

            return View(OfficeHourView);
        }

        // GET: /OfficeHour/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditOfficeHour(int id)
        {
            //  if (id == null)
            //  {
            //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //  }
            var officehour = db.OfficeHours.Find(id);

            if (officehour == null)
            {
                return HttpNotFound();
            }
            return View(officehour);
        }

        // POST: /OfficeHour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        //        public async Task<ActionResult> EditOfficeHour([Bind(Include = "UserName,FirstName,LastName,Address1,Address2,City,State,Country,Zip,Phone")] UserProfile userprofile)
        public async Task<ActionResult> EditOfficeHour(OfficeHour officehour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officehour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("OfficeHour");
            }
            return View(officehour);
        }
        // GET: /OfficeHour/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditHoliday(int id)
        {
            //  if (id == null)
            //  {
            //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //  }
            var holiday = db.HolidayNotes.Find(id);

            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: /Holiday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        //        public async Task<ActionResult> EditOfficeHour([Bind(Include = "UserName,FirstName,LastName,Address1,Address2,City,State,Country,Zip,Phone")] UserProfile userprofile)
        public async Task<ActionResult> EditHoliday(HolidayNote holiday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holiday).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Officehour");
            }
            return View(holiday);
        }

        // GET: /OfficeHour/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditContactInformation(int id)
        {
            //  if (id == null)
            //  {
            //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //  }
            var contact = db.ContactInformations.Find(id);

            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Holiday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        //        public async Task<ActionResult> EditContactInformation([Bind(Include = "UserName,FirstName,LastName,Address1,Address2,City,State,Country,Zip,Phone")] UserProfile userprofile)
        public async Task<ActionResult> EditContactInformation(ContactInformation contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Contact");
            }
            return View(contact);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [RequireSsl]
        public ActionResult Contact()
        {
            return View(GetContactInfo());
        }
        private ContactInformation GetContactInfo()
        {
            var contactinformation = db.ContactInformations.ToList();
            if (!contactinformation.Any())
            {
                db.ContactInformations.Add(new ContactInformation()
                {
                    Address = "Somewhere",
                    City = "Somewhere",
                    State = "Somewhere",
                    PostalCode = "99999",
                    Telphone = "555-555-5555",
                    Fax = "555-555-5555"
                });
                db.SaveChanges();
                contactinformation = db.ContactInformations.ToList();
            }
            return contactinformation[0];
        }
        [Authorize(Roles ="Admin")]
        public ActionResult EditContact()
        {
            var contactinformation = db.ContactInformations.ToList();
            if (!contactinformation.Any())
            {
                db.ContactInformations.Add(new ContactInformation()
                {
                    Address = "Somewhere",
                    City = "Somewhere",
                    State = "Somewhere",
                    PostalCode = "99999",
                    Telphone = "555-555-5555",
                    Fax = "555-555-5555"
                });
                db.SaveChanges();
                contactinformation = db.ContactInformations.ToList();
            }

            return View(contactinformation[0]);
        }
        [RequireSsl]
        public ActionResult Map()
        {
            var contactinformation = db.ContactInformations.ToList();
            this.Response.Headers.Add("Access-Control-Request-Headers", "X-Requested-With");
            this.Response.Headers.Add("Access-Control-Allow-Methods", "GET");
            return View(contactinformation[0]);
        }
    }
}
