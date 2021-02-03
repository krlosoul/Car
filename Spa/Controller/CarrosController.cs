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
    [Route("api/carros")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private Db db { get; set; }

        public CarrosController(Db db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take, int usuario)
        {
            try
            {
                List<Carros> data = db.carros.Include(x => x.producto).Where(x => x.idUsuario == usuario && x.estado != 3).OrderBy(x => x.id).Skip(skip).Take(take).Select(x => new Carros
                {
                    producto = new Productos
                    {
                        referencia = x.producto.referencia,
                        descripcion = x.producto.descripcion,
                        valor = x.producto.valor
                    },
                    id = x.id,
                    referenciaProducto = x.referenciaProducto,
                    cantidad = x.cantidad,
                    total = x.total,
                    estado = x.estado,
                }).ToList();

                int count = db.carros.Count();

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
        public async Task<Carros> Post([FromBody] Carros carro)
        {
            try
            {
                Carros tmpcarro = db.carros.FirstOrDefault(c => c.referenciaProducto == carro.referenciaProducto && c.idUsuario == carro.idUsuario);

                if (tmpcarro != default(Carros))
                {
                    throw new CustomException("El producto de referencia '" + tmpcarro.referenciaProducto + "' ya fue adquirido.");
                }

                db.carros.Add(carro);

                await db.SaveChangesAsync();

                return carro;

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

        [HttpPut("producto/{id}/{usuario}")]
        public async Task<Carros> Put(string id, int usuario, [FromBody] JsonElement carro)
        {
            try
            {
                Carros patch = db.carros.FirstOrDefault(c => c.referenciaProducto == id && c.idUsuario == usuario && c.estado != 3);

                if (patch == default(Carros))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }

                var json = carro.GetRawText();
                Carros _carro = JsonSerializer.Deserialize<Carros>(json);

                patch.cantidad = _carro.cantidad;
                patch.estado = _carro.estado;
                patch.total = _carro.total;

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
        [HttpDelete("producto/{id}")]
        public async Task<Carros> Delete(string id)
        {
            try
            {
                Carros patch = db.carros.FirstOrDefault(c => c.referenciaProducto == id);

                if (patch == default(Carros))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                db.carros.Remove(patch);
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

        [HttpDelete("limpiar/{id}")]
        public async Task<Carros> Clear(int id)
        {
            try
            {
                Carros patch = db.carros.FirstOrDefault(c => c.idUsuario == id && c.estado != 3);

                if (patch == default(Carros))
                {
                    throw new CustomException("No hay productos que eliminar");
                }

                db.carros.RemoveRange(db.carros.Where(x => x.idUsuario == id && x.estado != 3));
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
        [HttpPut("guardar/{id}")]
        public async Task<Carros> Guardar(int id, [FromBody] JsonElement carro)
        {
            try
            {
                Carros find = db.carros.FirstOrDefault(c => c.idUsuario == id && c.estado != 3);

                if (find == default(Carros))
                {
                    throw new CustomException("El carro se encuentra vacio");
                }

                db.carros.Where(x => x.idUsuario == id && x.estado != 3).ToList().ForEach(a => a.estado = 3);

                await db.SaveChangesAsync();

                Carros patch = db.carros.FirstOrDefault(c => c.idUsuario == id && c.estado == 3);

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

        [HttpGet("historial")]
        public GridModel GetHistorial([FromQuery] int skip, int take, int usuario)
        {
            try
            {
                List<Carros> data = db.carros.Include(x => x.producto).Where(x => x.idUsuario == usuario && x.estado == 3).OrderBy(x => x.estado).Skip(skip).Take(take).Select(x => new Carros
                {
                    producto = new Productos
                    {
                        referencia = x.producto.referencia,
                        descripcion = x.producto.descripcion,
                        valor = x.producto.valor
                    },
                    id = x.id,
                    referenciaProducto = x.referenciaProducto,
                    cantidad = x.cantidad,
                    total = x.total,
                    estado = x.estado,
                }).ToList();

                int count = db.carros.Count();

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

    }
}
