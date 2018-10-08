using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonApp.Models;

namespace PersonApp.Controllers
{
    public class custom_settingController : Controller
    {
        private RegisterDBEntities db = new RegisterDBEntities();

        // GET: custom_setting
        public ActionResult Index()
        {
            var custom_setting = db.custom_setting.Include(c => c.AspNetUser);
            return View(custom_setting.ToList());
        }

        // GET: custom_setting/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custom_setting custom_setting = db.custom_setting.Find(id);
            if (custom_setting == null)
            {
                return HttpNotFound();
            }
            return View(custom_setting);
        }

        // GET: custom_setting/Create
        public ActionResult Create()
        {
            string user_email = User.Identity.Name;
            var user_id = db.AspNetUsers.Where(x => x.Email == user_email).FirstOrDefault().Id;

            ViewBag.currentID = user_id;

            var cs = db.custom_setting.Where(x => x.ID == user_id).FirstOrDefault();
            if (cs != null)
            {
                return RedirectToAction("Edit", new { id = user_id });
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: custom_setting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,admin_email,smtp_domain,port,notify_email,theme")] custom_setting custom_setting)
        {
            if (ModelState.IsValid)
            {
                db.custom_setting.Add(custom_setting);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Personals");
            }

            return RedirectToAction("Dashboard", "Personals");
        }

        // GET: custom_setting/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custom_setting custom_setting = db.custom_setting.Find(id);
            if (custom_setting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", custom_setting.ID);
            return View(custom_setting);
        }

        // POST: custom_setting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,admin_email,smtp_domain,port,notify_email,theme")] custom_setting custom_setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custom_setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Personals");
            }
            return RedirectToAction("Dashboard", "Personals");
        }

        // GET: custom_setting/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custom_setting custom_setting = db.custom_setting.Find(id);
            if (custom_setting == null)
            {
                return HttpNotFound();
            }
            return View(custom_setting);
        }

        // POST: custom_setting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            custom_setting custom_setting = db.custom_setting.Find(id);
            db.custom_setting.Remove(custom_setting);
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
