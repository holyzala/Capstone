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
    public class ScoresheetsController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Scoresheets
        public IQueryable<Scoresheet> GetScoresheets()
        {
            return db.Scoresheets;
        }

        // GET: api/Scoresheets/5
        [ResponseType(typeof(Scoresheet))]
        public IHttpActionResult GetScoresheet(int id)
        {
            Scoresheet scoresheet = db.Scoresheets.Find(id);
            if (scoresheet == null)
            {
                return NotFound();
            }

            return Ok(scoresheet);
        }

        // PUT: api/Scoresheets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScoresheet(int id, Scoresheet scoresheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scoresheet.Score_ID)
            {
                return BadRequest();
            }

            db.Entry(scoresheet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoresheetExists(id))
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

        // POST: api/Scoresheets
        [ResponseType(typeof(Scoresheet))]
        public IHttpActionResult PostScoresheet(Scoresheet scoresheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scoresheets.Add(scoresheet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = scoresheet.Score_ID }, scoresheet);
        }

        // DELETE: api/Scoresheets/5
        [ResponseType(typeof(Scoresheet))]
        public IHttpActionResult DeleteScoresheet(int id)
        {
            Scoresheet scoresheet = db.Scoresheets.Find(id);
            if (scoresheet == null)
            {
                return NotFound();
            }

            db.Scoresheets.Remove(scoresheet);
            db.SaveChanges();

            return Ok(scoresheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScoresheetExists(int id)
        {
            return db.Scoresheets.Count(e => e.Score_ID == id) > 0;
        }
    }
}