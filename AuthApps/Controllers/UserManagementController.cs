using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AuthApps.Data;
using AuthApps.Models;

namespace AuthApps.Controllers
{
    [Route("api/{controller}/{action}/{id?}")]
    public class UserManagementController : ApiController
    {
        private DbAuthAppContext db = new DbAuthAppContext();

        // GET: api/UserManagement
        public IQueryable<t_user_login> Getuser_login()
        {
            return db.user_login;
        }

        // GET: api/UserManagement/5
        [ResponseType(typeof(t_user_login))]
        public IHttpActionResult Gett_user_login(int id)
        {
            t_user_login t_user_login = db.user_login.Find(id);
            if (t_user_login == null)
            {
                return NotFound();
            }

            return Ok(t_user_login);
        }

        // PUT: api/UserManagement/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_user_login(int id, t_user_login t_user_login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_user_login.id)
            {
                return BadRequest();
            }

            db.Entry(t_user_login).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_user_loginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserManagement
        [ResponseType(typeof(t_user_login))]
        public IHttpActionResult Postt_user_login(t_user_login t_user_login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.user_login.Add(t_user_login);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = t_user_login.id }, t_user_login);
        }

        // DELETE: api/UserManagement/5
        [ResponseType(typeof(t_user_login))]
        public IHttpActionResult Deletet_user_login(int id)
        {
            t_user_login t_user_login = db.user_login.Find(id);
            if (t_user_login == null)
            {
                return NotFound();
            }

            db.user_login.Remove(t_user_login);
            db.SaveChanges();

            return Ok(t_user_login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_user_loginExists(int id)
        {
            return db.user_login.Count(e => e.id == id) > 0;
        }
    }
}