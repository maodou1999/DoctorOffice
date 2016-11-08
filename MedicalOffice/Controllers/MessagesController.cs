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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Data.Entity.Validation;
using CherryCitySoftware.MedicalOffice.extensions;

namespace CherryCitySoftware.MedicalOffice.Controllers
{
    [RequireSsl]
    public class MessagesController : Controller
    {
         private ApplicationUserManager _userManager;

        public MessagesController()
        {
        }

        public MessagesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
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

        // GET: Messages
   /*     public async Task<ActionResult> Index(string Id)
        {
            var user = db.Users.Find(Id);
            var PatientMessage = user.Messages;
            return View(PatientMessage);
        }*/

        [Authorize]
        public ActionResult Index()
        {
            

            var messages = db.Messages.Where(m => m.Recepient.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase) && m.Sender!=null);
            var model = messages.Select(m => new MessageListViewModel()
                {
                    Subject = m.Subject,
                    Message = m.Content.Substring(0, m.Content.Length>200? 200 : m.Content.Length),
                    Date = m.Date,
                    Id = m.Id,
                    UserName =  m.Sender.LastName + ", " + m.Sender.FirstName,
                    UserId = m.Sender.Id,
                    ReadByRecepient = m.ReadByRecepient
                }).OrderByDescending(m=>m.Date).ToList();

            return View(model);
        }

        [Authorize]
        public ActionResult  Sent()
        {


            var messages = db.Messages.Where(m => m.Sender.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase) && m.Recepient != null);
            var model = messages.Select(m => new MessageListViewModel()
            {
                Subject = m.Subject,
                Message = m.Content.Substring(0, m.Content.Length > 200 ? 200 : m.Content.Length),
                Date = m.Date,
                Id = m.Id,
                UserName = m.Recepient.LastName + ", " + m.Recepient.FirstName,
                UserId = m.Recepient.Id
            }).OrderByDescending(m=>m.Date).ToList();
           
            return View(model);
        }
        // GET: Messages/Details/5
        [Authorize]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null || !User.Identity.GetUserId().Equals(db.Messages.Where(m=>m.Id==id.Value)
                .Select(m=>m.Recepient.Id).SingleOrDefault(), StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpNotFound();
            }
            message.ReadByRecepient = true;
            db.Entry(message).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(message);
        }

        // GET: SentMessages/Details/5
        [Authorize]
        public async Task<ActionResult> SentMessageDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);

            if (message == null || !User.Identity.GetUserId().Equals(db.Messages.Where(m=>m.Id==id.Value)
                .Select(m=>m.Sender.Id).SingleOrDefault(), StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpNotFound();
            }
            return View(message);
        }
        // GET: Admin Messages/Create
        [Authorize]
        public ActionResult AdminSentMessage(string recepientId)
        {

            var recepient = db.Users.FirstOrDefault(u => u.Id.Equals(recepientId, StringComparison.InvariantCultureIgnoreCase));

            return View(new Message() { Date = DateTime.Today, Recepient = recepient });
        }
        // GET: Messages/Create
        [Authorize]
        public ActionResult Create(String recepientId)
        { 
            return View(new CreateMessageViewModel() {RecepientId = recepientId});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> AdminSentMessage([Bind(Include = "Id,Subject,Content,Date")] Message message, FormCollection form, List<HttpPostedFileBase> attachedFile)
        {
           
            if (ModelState.IsValid)
            {
              //  var recepient = form.GetValue("Recepient.Id").AttemptedValue.ToString();

                message.Sender = await db.Users.FirstAsync(u => u.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
                
                var recepientId = form["Recepient.Id"];
               message.Recepient = await db.Users.FirstAsync(u => u.Id.Equals(recepientId, StringComparison.InvariantCultureIgnoreCase));
                //ApplicationUserManager um = new ApplicationUserManager(new);
                message.Date = DateTime.Now;
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                int filenumbers = attachedFile.Count();
                if (filenumbers >= 1)
                {
                    foreach (var uploadfile in attachedFile)
                    {
                        if (uploadfile != null)
                        {
                            byte[] buff = new byte[uploadfile.ContentLength];
                            int len = await uploadfile.InputStream.ReadAsync(buff, 0, uploadfile.ContentLength);
                            DocFile df = new DocFile()
                            {
                                Description = "Message attachment",
                                FileType = uploadfile.ContentType,
                                FileName = uploadfile.FileName,
                                Content = buff,
                                Message = message
                            };
                            db.DocFiles.Add(df);
                            //await db.SaveChangesAsync();
                        }
                    }
                }
                await db.SaveChangesAsync();
                //             await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
 //               await UserManager.SendEmailAsync(message.Recepient.Id, "New Message From", " Please login in and check ");

              //  var code = await UserManager.GeneratePasswordResetTokenAsync(recepientId);
           //     var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(recepientId, "New Message From Dr. Offfice", "Please login you account and check your message");
             //   ViewBag.Link = callbackUrl;
                return RedirectToAction("Index","Account");
            }

            return View(message);
        }
        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "Subject,Content,RecepientId")] CreateMessageViewModel model, List<HttpPostedFileBase> attachedFile)
        {

            if (ModelState.IsValid)
            {

                Message message = new Message();
                message.Sender = await db.Users.FirstAsync(u => u.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

                message.Recepient = await db.Users.FirstAsync(u => u.Id.Equals(model.RecepientId, StringComparison.InvariantCultureIgnoreCase)); 
                //ApplicationUserManager um = new ApplicationUserManager(new);
                message.Subject = model.Subject;
                message.Content = model.Content;

                message.Date = DateTime.Now;
                try
                {
                    db.Messages.Add(message);
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {
                    ex.LogException();
                    throw;
                }
                int filenumbers = attachedFile.Count();
                if (filenumbers >= 1)
                {
                    foreach (var uploadfile in attachedFile)
                    {
                        if (uploadfile != null)
                        {
                            byte[] buff = new byte[uploadfile.ContentLength];
                            int len = await uploadfile.InputStream.ReadAsync(buff, 0, uploadfile.ContentLength);
                            DocFile df = new DocFile()
                            {
                                Description = "Message attachment",
                                FileType = uploadfile.ContentType,
                                FileName = uploadfile.FileName,
                                Content = buff,
                                Message = message
                            };
                            db.DocFiles.Add(df);
                            //await db.SaveChangesAsync();
                        }
                    }
                }
               await db.SaveChangesAsync();
   //             await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                await UserManager.SendEmailAsync(message.Recepient.Id, "New Message From", string.Format(" Please login {0} to check your secure message box", Request.Url.Host));
                return RedirectToAction("Index", new { id=message.Recepient.Id });
            }

            return View(model);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public async Task<ActionResult> Reply(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageReplyViewModel message = await db.Messages.Where(m => m.Id == id.Value && m.Sender!=null)
                .Select(m => new MessageReplyViewModel()
            {
                Content = m.Sender.FirstName + " " + m.Sender.LastName + " Wrote\r\n" + m.Content,
                Subject = m.Subject,
                Id = m.Id,
                OriginalDate = m.Date
            }).SingleAsync();
            if (message == null)
            {
                return HttpNotFound();
            }

            message.Content = string.Format("\r\nOn {0} {1}", message.OriginalDate, message.Content).Replace("\r\n", "\r\n>");

            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Reply([Bind(Include = "Id,Subject,Content,Date")]  MessageReplyViewModel message, List<HttpPostedFileBase> attachedFile)
        {
            if (ModelState.IsValid)
            {
                var sender = await db.Messages.Where(m => m.Id == message.Id && m.Sender != null && m.Recepient != null)
                    .Select(m => new { SenderId = m.Sender.Id }).SingleAsync();
                Message messagereply =new Message();
                messagereply.Recepient = db.Users.Find(sender.SenderId);

                messagereply.Sender = await db.Users.Where(u => u.UserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)).SingleAsync();
                
                messagereply.Subject = message.Subject;
                messagereply.Content = message.Content;
                messagereply.Date = DateTime.Now;
                db.Messages.Add(messagereply);

                await db.SaveChangesAsync();
                await UserManager.SendEmailAsync(messagereply.Recepient.Id, "New Message From", string.Format(" Please login  {0} to check your secure message box", Request.Url.Host));
                int filenumbers = attachedFile.Count();
                if (filenumbers >= 1)
                {
                    foreach (var uploadfile in attachedFile)
                    {
                        if (uploadfile != null)
                        {
                            byte[] buff = new byte[uploadfile.ContentLength];
                            int len = await uploadfile.InputStream.ReadAsync(buff, 0, uploadfile.ContentLength);
                            DocFile df = new DocFile()
                            {
                                Description = "Message attachment",
                                FileType = uploadfile.ContentType,
                                FileName = uploadfile.FileName,
                                Content = buff,
                                Message = messagereply
                            };
                            db.DocFiles.Add(df);
                            //await db.SaveChangesAsync();
                        }
                    }
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Message message = await db.Messages.FindAsync(id);
            //if (message == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(message);
            var userId = User.Identity.GetUserId();
            Message message = await db.Messages.Include(m => m.Recepient).Include(m => m.DocFile).FirstAsync(m => m.Id == id && m.Recepient.Id.Equals(userId, StringComparison.InvariantCultureIgnoreCase));
            //Message message = await db.Messages.FindAsync(id);
            //Message message= await db.Messages.Include(m=>m.DocFile ).FirstAsync(m=>m.Id==id);
            var doc = message.DocFile;
            if (doc != null)
            {
                var list = doc.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    db.DocFiles.Remove(list[i]);
                }

            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            var userId = User.Identity.GetUserId();
            Message message = await db.Messages.Include(m => m.Recepient).Include(m=>m.DocFile).FirstAsync(m => m.Id == id && m.Recepient.Id.Equals(userId, StringComparison.InvariantCultureIgnoreCase));
            //Message message = await db.Messages.FindAsync(id);
            //Message message= await db.Messages.Include(m=>m.DocFile ).FirstAsync(m=>m.Id==id);
            var doc = message.DocFile;
            if (doc != null)
            {
                var list = doc.ToList();
                for (int i=0; i<list.Count; i++)
                {
                    


                        db.DocFiles.Remove(list[i]);
                }


            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();
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
