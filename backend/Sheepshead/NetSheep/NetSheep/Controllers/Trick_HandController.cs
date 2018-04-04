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
    public class Trick_HandController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Trick_Hand
        public IQueryable<Trick_Hand> GetTrick_Hand()
        {
            return db.Trick_Hand;
        }

        // GET: api/Trick_Hand/5
        [ResponseType(typeof(Trick_Hand))]
        public IHttpActionResult GetTrick_Hand(int id)
        {
            Trick_Hand trick_Hand = db.Trick_Hand.Find(id);
            if (trick_Hand == null)
            {
                return NotFound();
            }

            return Ok(trick_Hand);
        }

        // PUT: api/Trick_Hand/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrick_Hand(int id, Trick_Hand trick_Hand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trick_Hand.Trick_Round)
            {
                return BadRequest();
            }

            db.Entry(trick_Hand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Trick_HandExists(id))
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

        // POST: api/Trick_Hand
        [ResponseType(typeof(Trick_Hand))]
        public IHttpActionResult PostTrick_Hand(Trick_Hand trick_Hand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trick_Hand.Add(trick_Hand);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Trick_HandExists(trick_Hand.Trick_Round))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = trick_Hand.Trick_Round }, trick_Hand);
        }

        // DELETE: api/Trick_Hand/5
        [ResponseType(typeof(Trick_Hand))]
        public IHttpActionResult DeleteTrick_Hand(int id)
        {
            Trick_Hand trick_Hand = db.Trick_Hand.Find(id);
            if (trick_Hand == null)
            {
                return NotFound();
            }

            db.Trick_Hand.Remove(trick_Hand);
            db.SaveChanges();

            return Ok(trick_Hand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Trick_HandExists(int id)
        {
            return db.Trick_Hand.Count(e => e.Trick_Round == id) > 0;
        }
    }
}