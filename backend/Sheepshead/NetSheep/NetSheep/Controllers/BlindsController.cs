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
    public class BlindsController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Blinds
        public IQueryable<Blind> GetBlinds()
        {
            return db.Blinds;
        }

        // GET: api/Blinds/5
        [ResponseType(typeof(Blind))]
        public IHttpActionResult GetBlind(int id)
        {
            Blind blind = db.Blinds.Find(id);
            if (blind == null)
            {
                return NotFound();
            }

            return Ok(blind);
        }

        // PUT: api/Blinds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlind(int id, Blind blind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blind.Blind_Game)
            {
                return BadRequest();
            }

            db.Entry(blind).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlindExists(id))
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

        // POST: api/Blinds
        [ResponseType(typeof(Blind))]
        public IHttpActionResult PostBlind(Blind blind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blinds.Add(blind);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BlindExists(blind.Blind_Game))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = blind.Blind_Game }, blind);
        }

        // DELETE: api/Blinds/5
        [ResponseType(typeof(Blind))]
        public IHttpActionResult DeleteBlind(int id)
        {
            Blind blind = db.Blinds.Find(id);
            if (blind == null)
            {
                return NotFound();
            }

            db.Blinds.Remove(blind);
            db.SaveChanges();

            return Ok(blind);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlindExists(int id)
        {
            return db.Blinds.Count(e => e.Blind_Game == id) > 0;
        }
    }
}