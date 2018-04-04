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
    public class Table_MatchController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Table_Match
        public IQueryable<Table_Match> GetTable_Match()
        {
            return db.Table_Match;
        }

        // GET: api/Table_Match/5
        [ResponseType(typeof(Table_Match))]
        public IHttpActionResult GetTable_Match(int id)
        {
            Table_Match table_Match = db.Table_Match.Find(id);
            if (table_Match == null)
            {
                return NotFound();
            }

            return Ok(table_Match);
        }

        // PUT: api/Table_Match/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable_Match(int id, Table_Match table_Match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Match.Table_ID)
            {
                return BadRequest();
            }

            db.Entry(table_Match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_MatchExists(id))
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

        // POST: api/Table_Match
        [ResponseType(typeof(Table_Match))]
        public IHttpActionResult PostTable_Match(Table_Match table_Match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Match.Add(table_Match);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = table_Match.Table_ID }, table_Match);
        }

        // DELETE: api/Table_Match/5
        [ResponseType(typeof(Table_Match))]
        public IHttpActionResult DeleteTable_Match(int id)
        {
            Table_Match table_Match = db.Table_Match.Find(id);
            if (table_Match == null)
            {
                return NotFound();
            }

            db.Table_Match.Remove(table_Match);
            db.SaveChanges();

            return Ok(table_Match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_MatchExists(int id)
        {
            return db.Table_Match.Count(e => e.Table_ID == id) > 0;
        }
    }
}