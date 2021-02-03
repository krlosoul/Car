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
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private Db db { get; set; }

        public ProductosController(Db db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                List<Productos> data = db.productos.OrderBy(x => x.descripcion).Skip(skip).Take(take).ToList();
                int count = db.productos.Count();
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
        public async Task<Productos> Post([FromBody] Productos producto)
        {
            try
            {
                db.productos.Add(producto);
                await db.SaveChangesAsync();
                return producto;
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
        public async Task<Productos> Put(string id, [FromBody] Productos producto)
        {
            try
            {
                Productos patch = db.productos.Find(id);

                if (patch == default(Productos))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                patch.descripcion = producto.descripcion;
                patch.valor = producto.valor;

                await db.SaveChangesAsync();
                return producto;
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
        public async Task<Productos> Delete(string id)
        {
            try
            {
                Productos patch = db.productos.Find(id);

                if (patch == default(Productos))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                db.productos.Remove(patch);
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
