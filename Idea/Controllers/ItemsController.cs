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
using Idea.Models;

namespace Idea.Controllers
{
    public class ItemsController : ApiController
    {
        private IdeaEntities db = new IdeaEntities();

        // GET: api/Items
        [Route("api/Items/{locationName}/{productId}")]
        public IQueryable<Item> GetItems(string locationName, int productId)
        {
            Location loc = db.Locations.Where(l => l.Name == locationName).FirstOrDefault();
            return db.Items.Where(i => i.FK_LocationId==loc.Id && i.FK_ProductId==productId);
        }
        // GET: api/Items/5
        [Route("api/LocationItems/{locationName}")]
        public IQueryable<Item> GetLocationItems(string locationName)
        {
            IQueryable<Item> filteredItems = null;
            Location loc = db.Locations.Where(l => l.Name == locationName).FirstOrDefault();
            if (loc != null)
            {
                filteredItems = db.Items.Where(i => i.FK_LocationId == loc.Id ).AsQueryable<Item>();
                if (filteredItems == null)
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

            return filteredItems;
        }
        // GET: api/Items/5
        [Route("api/filteredItems/{locationName}/{product}")]
        public IQueryable<Item> GetFilteredItems(string locationName,int product)
        {
            IQueryable<Item> filteredItems = null;
            Location loc = db.Locations.Where(l => l.Name == locationName).FirstOrDefault();
            if (loc !=null)
            {
                filteredItems = db.Items.Where(i => i.FK_LocationId == loc.Id && i.FK_ProductId==product).AsQueryable<Item>();
                if (filteredItems == null)
                {
                    return null;
                }
                
            }
            else 
            {
                return null;
            }

            return filteredItems;
        }
        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(long id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(long id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(long id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(long id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}