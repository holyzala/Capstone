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
using NetSheep.Models;

namespace NetSheep.Controllers
{
    public class HumenController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Humen
        public IQueryable<Human> GetHumen()
        {
            return db.Humen;
        }

        // GET: api/Humen/5
        [ResponseType(typeof(Human))]
        public IHttpActionResult GetHuman(int id)
        {
            Human human = db.Humen.Find(id);
            if (human == null)
            {
                return NotFound();
            }

            return Ok(human);
        }

        // PUT: api/Humen/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHuman(int id, Human human)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != human.Player_ID)
            {
                return BadRequest();
            }

            db.Entry(human).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanExists(id))
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

        // POST: api/Humen
        [ResponseType(typeof(Human))]
        public IHttpActionResult PostHuman(Human human)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Humen.Add(human);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HumanExists(human.Player_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = human.Player_ID }, human);
        }

        // DELETE: api/Humen/5
        [ResponseType(typeof(Human))]
        public IHttpActionResult DeleteHuman(int id)
        {
            Human human = db.Humen.Find(id);
            if (human == null)
            {
                return NotFound();
            }

            db.Humen.Remove(human);
            db.SaveChanges();

            return Ok(human);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HumanExists(int id)
        {
            return db.Humen.Count(e => e.Player_ID == id) > 0;
        }
    }
}