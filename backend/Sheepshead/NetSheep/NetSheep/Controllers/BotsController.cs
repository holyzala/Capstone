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
    public class BotsController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Bots
        public IQueryable<Bot> GetBots()
        {
            return db.Bots;
        }

        // GET: api/Bots/5
        [ResponseType(typeof(Bot))]
        public IHttpActionResult GetBot(int id)
        {
            Bot bot = db.Bots.Find(id);
            if (bot == null)
            {
                return NotFound();
            }

            return Ok(bot);
        }

        // PUT: api/Bots/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBot(int id, Bot bot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bot.Player_ID)
            {
                return BadRequest();
            }

            db.Entry(bot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotExists(id))
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

        // POST: api/Bots
        [ResponseType(typeof(Bot))]
        public IHttpActionResult PostBot(Bot bot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bots.Add(bot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BotExists(bot.Player_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bot.Player_ID }, bot);
        }

        // DELETE: api/Bots/5
        [ResponseType(typeof(Bot))]
        public IHttpActionResult DeleteBot(int id)
        {
            Bot bot = db.Bots.Find(id);
            if (bot == null)
            {
                return NotFound();
            }

            db.Bots.Remove(bot);
            db.SaveChanges();

            return Ok(bot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BotExists(int id)
        {
            return db.Bots.Count(e => e.Player_ID == id) > 0;
        }
    }
}