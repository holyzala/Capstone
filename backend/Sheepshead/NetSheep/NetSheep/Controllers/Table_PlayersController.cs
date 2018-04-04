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
    public class Table_PlayersController : ApiController
    {
        private Sheep db = new Sheep();

        // GET: api/Table_Players
        public IQueryable<Table_Players> GetTable_Players()
        {
            return db.Table_Players;
        }

        // GET: api/Table_Players/5
        [ResponseType(typeof(Table_Players))]
        public IHttpActionResult GetTable_Players(int id)
        {
            Table_Players table_Players = db.Table_Players.Find(id);
            if (table_Players == null)
            {
                return NotFound();
            }

            return Ok(table_Players);
        }

        // PUT: api/Table_Players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable_Players(int id, Table_Players table_Players)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Players.TP_Player_Id)
            {
                return BadRequest();
            }

            db.Entry(table_Players).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_PlayersExists(id))
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

        // POST: api/Table_Players
        [ResponseType(typeof(Table_Players))]
        public IHttpActionResult PostTable_Players(Table_Players table_Players)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Players.Add(table_Players);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Table_PlayersExists(table_Players.TP_Player_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = table_Players.TP_Player_Id }, table_Players);
        }

        // DELETE: api/Table_Players/5
        [ResponseType(typeof(Table_Players))]
        public IHttpActionResult DeleteTable_Players(int id)
        {
            Table_Players table_Players = db.Table_Players.Find(id);
            if (table_Players == null)
            {
                return NotFound();
            }

            db.Table_Players.Remove(table_Players);
            db.SaveChanges();

            return Ok(table_Players);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_PlayersExists(int id)
        {
            return db.Table_Players.Count(e => e.TP_Player_Id == id) > 0;
        }
    }
}