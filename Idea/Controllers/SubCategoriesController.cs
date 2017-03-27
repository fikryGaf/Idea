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
    public class SubCategoriesController : ApiController
    {
        private IdeaEntities db = new IdeaEntities();

        // GET: api/SubCategories
        public IQueryable<SubCategory> GetSubCategories()
        {
            return db.SubCategories;
        }

        // GET: api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public IHttpActionResult GetSubCategory(long id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return Ok(subCategory);
        }

        // GET: api/SubCategory/{categoryId}
        [Route("api/SubCategory/{categoryId}")]
        [HttpGet]
        public IHttpActionResult GetSubCategoryItems(long categoryId=0)
        {
            IQueryable<SubCategory> subCategoryItems = db.SubCategories.Where(c => c.FK_CategoryID == categoryId);
            if (subCategoryItems == null)
            {
                return NotFound();
            }

            return Ok(subCategoryItems);
        }
        // PUT: api/SubCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubCategory(long id, SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(subCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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

        // POST: api/SubCategories
        [ResponseType(typeof(SubCategory))]
        public IHttpActionResult PostSubCategory(SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubCategories.Add(subCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subCategory.Id }, subCategory);
        }

        // DELETE: api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public IHttpActionResult DeleteSubCategory(long id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            db.SubCategories.Remove(subCategory);
            db.SaveChanges();

            return Ok(subCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoryExists(long id)
        {
            return db.SubCategories.Count(e => e.Id == id) > 0;
        }
    }
}