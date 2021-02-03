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
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private Db db { get; set; }

        public UsuariosController(Db db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                List<Usuarios> data = db.usuarios.Include(x => x.persona).OrderBy(x => x.persona.apellidos).Skip(skip).Take(take).Select(x => new Usuarios
                {
                    persona = new Personas
                    {
                        id = x.persona.id,
                        nombres = x.persona.nombres,
                        apellidos = x.persona.apellidos
                    },
                    id = x.id,
                    idPersona = x.persona.id,
                    usuario = x.usuario,
                    clave = x.clave

                }).ToList();

                int count = db.usuarios.Count();

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
        public async Task<Usuarios> Post([FromBody] Usuarios usuario)
        {
            try
            {
                Usuarios tmpusuario = db.usuarios.FirstOrDefault(u => u.usuario == usuario.usuario);

                if (tmpusuario != default(Usuarios))
                {
                    throw new CustomException("El usuario '" + tmpusuario.usuario + "' ya existe.");
                }

                Personas persona = usuario.persona;

                db.personas.Add(usuario.persona);

                await db.SaveChangesAsync();

                usuario.idPersona = persona.id;

                db.usuarios.Add(usuario);

                await db.SaveChangesAsync();

                return usuario;

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
        public async Task<Usuarios> Put(int id, [FromBody] Usuarios usuario)
        {
            try
            {
                Usuarios patch = db.usuarios.Find(id);

                if (patch == default(Usuarios))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                patch.usuario = usuario.usuario;

                await db.SaveChangesAsync();
                return usuario;
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
        public async Task<Usuarios> Delete(int id)
        {
            try
            {
                Usuarios patch = db.usuarios.Find(id);

                if (patch == default(Usuarios))
                {
                    throw new CustomException("El registro ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                db.usuarios.Remove(patch);
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

        [Route("login")]
        [HttpPost]
        public async Task<Usuarios> Login([FromBody] Usuarios usuario)
        {
            try
            {
                Usuarios findUsuario = await db.usuarios.FirstOrDefaultAsync(u => u.usuario == usuario.usuario);
                if (findUsuario == default(Usuarios))
                {
                    throw new CustomException("Usuario y/o contraseña incorrectos");
                }
                if (findUsuario.clave != usuario.clave)
                {
                    throw new CustomException("Usuario y/o contraseña incorrectos");
                }
                return findUsuario;
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
