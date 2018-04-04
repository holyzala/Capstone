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
    public class HandsController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Hands
        public IQueryable<Hand> GetHands()
        {
            return db.Hands;
        }

        // GET: api/Hands/5
        [ResponseType(typeof(Hand))]
        public IHttpActionResult GetHand(int id)
        {
            Hand hand = db.Hands.Find(id);
            if (hand == null)
            {
                return NotFound();
            }

            return Ok(hand);
        }

        // PUT: api/Hands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHand(int id, Hand hand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hand.Hand_Player)
            {
                return BadRequest();
            }

            db.Entry(hand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HandExists(id))
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

        // POST: api/Hands
        [ResponseType(typeof(Hand))]
        public IHttpActionResult PostHand(Hand hand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hands.Add(hand);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HandExists(hand.Hand_Player))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hand.Hand_Player }, hand);
        }

        // DELETE: api/Hands/5
        [ResponseType(typeof(Hand))]
        public IHttpActionResult DeleteHand(int id)
        {
            Hand hand = db.Hands.Find(id);
            if (hand == null)
            {
                return NotFound();
            }

            db.Hands.Remove(hand);
            db.SaveChanges();

            return Ok(hand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HandExists(int id)
        {
            return db.Hands.Count(e => e.Hand_Player == id) > 0;
        }
    }
}