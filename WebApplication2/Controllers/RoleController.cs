using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RoleController : Controller
    {
        private DbApps2Context db = new DbApps2Context();

        // GET: Role
        public ActionResult Index()
        {
            return View(db.roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_role t_role = db.roles.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,role_name")] t_role t_role)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(t_role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_role t_role = db.roles.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,role_name")] t_role t_role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_role t_role = db.roles.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_role t_role = db.roles.Find(id);
            db.roles.Remove(t_role);
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
