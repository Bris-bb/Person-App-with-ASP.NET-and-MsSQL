using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PersonApp.Models;

namespace PersonApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private RegisterDBEntities db = new RegisterDBEntities();


        public List<Event> getcurrentRole()
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

            var events = db.Events.Include(p => p.AspNetUser).Where(p => p.User_ID == user_id).ToList();
            return events;
        }


        // GET: Events
        public ActionResult Index()
        {
            var events = getcurrentRole();
            return View(events);
        }


        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.User_ID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User_ID,title,description,date,end_date,url")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.AspNetUsers, "Id", "Email", @event.User_ID);
            return View(@event);
        }

        public JsonResult ajaxCreate(Event e)
        {
            e = db.Events.Add(e);
            db.SaveChanges();
            return Json(new { id = e.ID}, JsonRequestBehavior.AllowGet );
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.AspNetUsers, "Id", "Email", @event.User_ID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_ID,title,description,date,end_date,url")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.AspNetUsers, "Id", "Email", @event.User_ID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult ajaxDelete(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return Json(new { res = "ok" }, JsonRequestBehavior.AllowGet);
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
