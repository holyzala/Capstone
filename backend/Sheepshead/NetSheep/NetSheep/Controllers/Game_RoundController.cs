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
    public class Game_RoundController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Game_Round
        public IQueryable<Game_Round> GetGame_Round()
        {
            return db.Game_Round;
        }

        // GET: api/Game_Round/5
        [ResponseType(typeof(Game_Round))]
        public IHttpActionResult GetGame_Round(int id)
        {
            Game_Round game_Round = db.Game_Round.Find(id);
            if (game_Round == null)
            {
                return NotFound();
            }

            return Ok(game_Round);
        }

        // PUT: api/Game_Round/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGame_Round(int id, Game_Round game_Round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game_Round.Round_ID)
            {
                return BadRequest();
            }

            db.Entry(game_Round).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_RoundExists(id))
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

        // POST: api/Game_Round
        [ResponseType(typeof(Game_Round))]
        public IHttpActionResult PostGame_Round(Game_Round game_Round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Game_Round.Add(game_Round);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Game_RoundExists(game_Round.Round_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = game_Round.Round_ID }, game_Round);
        }

        // DELETE: api/Game_Round/5
        [ResponseType(typeof(Game_Round))]
        public IHttpActionResult DeleteGame_Round(int id)
        {
            Game_Round game_Round = db.Game_Round.Find(id);
            if (game_Round == null)
            {
                return NotFound();
            }

            db.Game_Round.Remove(game_Round);
            db.SaveChanges();

            return Ok(game_Round);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Game_RoundExists(int id)
        {
            return db.Game_Round.Count(e => e.Round_ID == id) > 0;
        }
    }
}