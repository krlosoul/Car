using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using Entities.Collections;
using Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Spa.ApiController
{
    [Route("api/personas")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private Db db { get; set; }

        public PersonasController(Db db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                List<Personas> data = db.personas.OrderBy(x => x.nombres).Skip(skip).Take(take).ToList();
                int count = db.personas.Count();
                return new GridModel { data = data, count = count };
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpPost]
        public async Task<Personas> Post([FromBody] Personas persona)
        {
            try
            {
                db.personas.Add(persona);
                await db.SaveChangesAsync();
                return persona;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<Personas> Put(int id, [FromBody] Personas persona)
        {
            try
            {
                Personas patch = db.personas.Find(id);

                if (patch == default(Personas))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                patch.nombres = persona.nombres;
                patch.apellidos = persona.apellidos;

                await db.SaveChangesAsync();
                return persona;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<Personas> Delete(int id)
        {
            try
            {
                Personas patch = db.personas.Find(id);

                if (patch == default(Personas))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                db.personas.Remove(patch);
                await db.SaveChangesAsync();
                return patch;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

    }
}
