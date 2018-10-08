using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PersonApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PersonApp.Controllers
{
    [Authorize]
    public class PersonalsController : Controller
    {
        private RegisterDBEntities db = new RegisterDBEntities();

        // GET: Dashboard
        [Authorize(Roles="admin")]
        public ActionResult Dashboard()
        {
            getcurrentRole();

            var currentDate = DateTime.Now;
            var expires = db.Personals.Include(p => p.AspNetUser).
                Where(p => (p.Company_TL_Expire_Date < currentDate || p.ID_Expire_Date < currentDate || p.Passport_Expire_Date < currentDate));

            var notifies = db.Personals.Include(p => p.AspNetUser).
                            Where(p => (p.Company_TL_Expire_Date < currentDate || p.ID_Expire_Date < currentDate || p.Passport_Expire_Date < currentDate) && p.status == "expired");

            ViewBag.Insiders = db.Personals.Count();
            ViewBag.Expired = expires.Count();
            ViewBag.Notifies = notifies.Count();
            return View(notifies.ToList());
        }

        public JsonResult Upload(string User_Id, string Doc_type)
        {
            var file = Request.Files[0];

            var currentTime = DateTime.Now;
            string fileName = file.FileName;

            var fileExtension = Path.GetExtension(fileName);
            //currentTime.ToString("yyyy-MMMM-dd-hh:mm:ss.f")
            
            //var User_Id = Request.User_Id;
            fileName = Doc_type + User_Id + fileExtension;

            var path = Path.Combine(Server.MapPath("~/assets/upload/"), fileName);
                
            file.SaveAs(path);
            return Json(new { User_Id = User_Id, path = fileName, time = currentTime.ToString("yyyy-MMMM-dd-hh:mm:ss.f") }, JsonRequestBehavior.AllowGet);
        }
          
        // GET: Personals
        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            getcurrentRole();
            var personals = db.Personals.Include(p => p.AspNetUser);
            return View(personals.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Search()
        {
            getcurrentRole();

            var personals = db.Personals.Include(p => p.AspNetUser);
            return View(personals.ToList());
        }
        [Authorize(Roles = "admin")]
        public ActionResult Report()
        {
            getcurrentRole();

            var personals = db.Personals.Include(p => p.AspNetUser);
            return View(personals.ToList());
        }

        // GET: Personals/Notify
        [Authorize(Roles="admin")]
        public ActionResult Notify()
        {
            getcurrentRole();

            var currentDate = DateTime.Now;
            var personals = db.Personals.Include(p => p.AspNetUser).
                Where(p=> (p.Company_TL_Expire_Date<currentDate || p.ID_Expire_Date<currentDate || p.Passport_Expire_Date<currentDate) && p.status !="notify");
            var lst = personals.ToList();                
            for (var i=0; i<lst.Count(); i++)
            {
                lst.ElementAt(i).status = "expired";
            }
            db.SaveChanges();
            return View(personals.ToList());
        }

        // POST: Personals/Notify
        public async System.Threading.Tasks.Task<ActionResult> Send_Notify(string user_id)
        {
            var personal = db.Personals.Where(p => p.User_Id == (string)user_id).FirstOrDefault();
            personal.status = "notify";
            db.SaveChanges();

            var user = db.AspNetUsers.FirstOrDefault(u => u.Id == personal.User_Id);
            
            if (personal != null)
            {
                ViewBag.user_img = personal.ID_Upload;
                ViewBag.company_img = personal.Comany_TL_Upload;
            }

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("yayzooyama@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("nikisaitou@hotmail.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Test", "nikisaitou@hotmail.com", "Test Message");
            message.IsBodyHtml = true;

            using (var local_smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "notifications@ucarrot.com",       // replace with valid value
                    Password = "Not!fy@me"   // replace with valid value
                };
                local_smtp.Credentials = credential;
                local_smtp.Host = "mail.ucarrot.com";  //"smtp.mailgun.org";
                local_smtp.Port = 8889;
                local_smtp.EnableSsl = true;
                
                 await local_smtp.SendMailAsync(message);
                return RedirectToAction("Notify");
            }
        }


        // GET: Personals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        public Personal getcurrentRole()
        {
            string user_email = User.Identity.Name;
            var user_id = db.AspNetUsers.Where(x => x.Email == user_email).FirstOrDefault().Id;
            ViewBag.user_email = user_email;
            ViewBag.logged_user_id = user_id;
            var personal = db.Personals.Include(p => p.AspNetUser).Where(p => p.User_Id == user_id).FirstOrDefault();

            if (personal != null)
            {
                ViewBag.user_img = personal.ID_Upload;
                ViewBag.company_img = personal.Comany_TL_Upload;
                ViewBag.flg_exist = true;
                //RedirectToAction("UpdatePersonal");
            }
            else
            {
                ViewBag.flg_exist = false;
            }

            var role_id = db.AspNetUserRoles.Where(x => x.UserId == user_id).FirstOrDefault().RoleId;
            var role = db.AspNetRoles.Where(x => x.Id == role_id).FirstOrDefault().Name;
            ViewBag.role = role;
            return personal;
        }

        // GET: Personals/Create
        public ActionResult Create()
        {
            var personal = getcurrentRole();
            if (personal != null)
            {
                return RedirectToAction("UpdatePersonal");
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Insider_Name,Insider_Name_arabic,Company_Name,Company_Name_arabic,Position,Position_arabic,UAE_Resident,UAE_Resident_arabic,Emirates_ID,Passport_Number,Mobile_Number,Office_Number,User_Id,Email_Address_2,Trade_License,Security_Code,Other_information,ID_Expire_Date,ID_Upload,Passport_Expire_Date,Passport_Upload,Company_TL_Expire_Date,Comany_TL_Upload,status,Comany_Image_Upload")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Personals.Add(personal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", personal.User_Id);
            return View(personal);
        }

        // GET: Personals/UpdatePersonal
        [HttpGet]
        public ActionResult UpdatePersonal()
        {
            
            var personal = getcurrentRole();
            if (personal == null)
            {
                return RedirectToAction("Create");
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", personal.User_Id);
            return View(personal);
        }

        // GET: Personals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", personal.User_Id);
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Insider_Name,Insider_Name_arabic,Company_Name,Company_Name_arabic,Position,Position_arabic,UAE_Resident,UAE_Resident_arabic,Emirates_ID,Passport_Number,Mobile_Number,Office_Number,User_Id,Email_Address_2,Trade_License,Security_Code,Other_information,ID_Expire_Date,ID_Upload,Passport_Expire_Date,Passport_Upload,Company_TL_Expire_Date,Comany_TL_Upload,status,Comany_Image_Upload")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "Email", personal.User_Id);
            return View(personal);
        }

        // GET: Personals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personal personal = db.Personals.Find(id);
            db.Personals.Remove(personal);
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
