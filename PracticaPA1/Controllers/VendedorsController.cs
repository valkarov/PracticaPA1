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
using PracticaPA1.Models;
using PractivaPA1.DataB;

namespace PracticaPA1.Controllers
{
    public class VendedorsController : ApiController
    {
        private PrimaryDatabContext db = new PrimaryDatabContext();

        // GET: api/Vendedors
        public IQueryable<Vendedor> GetVendedores()
        {
            return db.Vendedores.Include(v => v.Vehiculo);
        }

        // GET: api/Vendedors/5
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult GetVendedor(long id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return Ok(vendedor);
        }

        // PUT: api/Vendedors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendedor(long id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendedor.IdVendedor)
            {
                return BadRequest();
            }

            db.Entry(vendedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
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

        // POST: api/Vendedors
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult PostVendedor(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendedores.Add(vendedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendedor.IdVendedor }, vendedor);
        }

        // DELETE: api/Vendedors/5
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult DeleteVendedor(long id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            db.Vendedores.Remove(vendedor);
            db.SaveChanges();

            return Ok(vendedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendedorExists(long id)
        {
            return db.Vendedores.Count(e => e.IdVendedor == id) > 0;
        }
    }
}