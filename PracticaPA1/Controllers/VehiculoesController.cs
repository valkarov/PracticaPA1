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
    public class VehiculoesController : ApiController
    {
        private PrimaryDatabContext db = new PrimaryDatabContext();

        // GET: api/Vehiculoes
        public IQueryable<Vehiculo> GetVehiculores()
        {
            return db.Vehiculores.Include(v => v.Vendedor);
        }

        // GET: api/Vehiculoes/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult GetVehiculo(long id)
        {
            Vehiculo vehiculo = db.Vehiculores.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }

        // PUT: api/Vehiculoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehiculo(long id, Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculo.IdVehiculo)
            {
                return BadRequest();
            }

            db.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
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

        // POST: api/Vehiculoes
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult PostVehiculo(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehiculores.Add(vehiculo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehiculo.IdVehiculo }, vehiculo);
        }

        // DELETE: api/Vehiculoes/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult DeleteVehiculo(long id)
        {
            Vehiculo vehiculo = db.Vehiculores.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            db.Vehiculores.Remove(vehiculo);
            db.SaveChanges();

            return Ok(vehiculo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiculoExists(long id)
        {
            return db.Vehiculores.Count(e => e.IdVehiculo == id) > 0;
        }
    }
}