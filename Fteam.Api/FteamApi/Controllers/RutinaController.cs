using FteamApi.Api.Atributos;
using FteamApi.Models.ViewModel;
using FteamLibrary.Enum;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutinaController : ControllerBase
    {

        RutinaService _rutinaService;
        EjercicioService _ejercicioService;
        UsuarioService _usuarioService;

        public RutinaController(RutinaService rs, EjercicioService es, UsuarioService us)
        {
            _rutinaService = rs;
            _ejercicioService = es;
            _usuarioService = us;

        }

        // GET: api/ejercicio/5/1
        [HttpGet("{idGimnasio}/{id}")]
        [HttpGet]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public Rutina Get(string idGimnasio, string id)
        {

            var r = _rutinaService.Get(idGimnasio, id);

            return Fill(r);
        }

        // POST api/rutina/1
        [HttpPost("{idGimnasio}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task Post(string idGimnasio, [FromBody] Rutina rutinas)
        {
            var r = Fill(idGimnasio, rutinas);

            await _rutinaService.InsertAsync(idGimnasio, r);
        }



        // PUT api/<RutinaController>/5
        [HttpPut("{idGimnasio}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task PutAsync(string idGimnasio, [FromBody] Rutina rutina)
        {

            var r = Fill(idGimnasio, rutina);

            await _rutinaService.UpdateAsync(idGimnasio, r);
        }

        // DELETE api/<RutinaController>/5
        [HttpDelete("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public void Delete(int id)
        {
        }




        #region Privado

        public FteamLibrary.Entities.Rutina Fill(string idGimnasio, Rutina rutina)
        {
            var r = new FteamLibrary.Entities.Rutina();

            r.Id = rutina.Id;
            r.Nombre = rutina.Nombre;
            r.Detalle = rutina.Detalle;

            r.TipoClase = (FteamLibrary.Entities.eTipoClase) rutina.TipoClase;

            List<FteamLibrary.Entities.Bloque> bloques = new List<FteamLibrary.Entities.Bloque>();

            var rb = rutina.EjercicioActividades.Where(x => x.Cabecera).OrderBy(x => x.Orden);

            foreach (var item in rb)
            {

                var b = new FteamLibrary.Entities.Bloque();

                b.Id = ObjectId.GenerateNewId().ToString();
                b.Nombre = item.NombreEjercicio;
                b.Duracion = item.Duracion;

                b.RepeticionBloque = item.RepeticionBloque;

                b.TipoBloque = item.TipoBloque;
                b.Orden = item.Bloque;

                foreach (var ea in rutina.EjercicioActividades.Where(x => x.Bloque == item.Bloque && x.Orden > 0))
                {
                    var e = _ejercicioService.Get(idGimnasio, ea.IdEjercicio);

                    b.EjercicioActividad.Add(new FteamLibrary.Entities.EjercicioActividad()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Bloque = ea.Bloque,
                        Orden = ea.Orden,
                        Ejercicio = e,
                        Repeticiones = ea.Repeticiones,
                        RepeticionesAvanzado = ea.RepeticionesAvanzado,
                        Segundos = ea.Segundos,
                        SegundosDescanso = ea.SegundosDescanso
                    });
                }

                bloques.Add(b);
            }
            r.Bloques = bloques;

            return r;

        }

        public Rutina Fill(FteamLibrary.Entities.Rutina rutina)
        {
            Rutina r = new Rutina();

            r.Id = rutina.Id;
            r.Nombre = rutina.Nombre;
            r.Detalle = rutina.Detalle;            
            r.TipoClase = (int)rutina.TipoClase;

            foreach (var item in rutina.Bloques.OrderBy(x => x.Orden))
            {

                r.EjercicioActividades.Add(new EjercicioActividad()
                {
                    Id = item.Id,
                    Bloque = item.Orden,
                    NombreEjercicio = item.Nombre,
                    Duracion = item.Duracion,
                    RepeticionBloque = item.RepeticionBloque,
                    Repeticiones = item.RepeticionBloque,
                    TipoBloque = item.TipoBloque,
                    Cabecera = true
                });

                if (item.EjercicioActividad != null)
                    foreach (var ejercicio in item.EjercicioActividad.OrderBy(x => x.Orden))
                    {
                        r.EjercicioActividades.Add(new EjercicioActividad()
                        {
                            IdEjercicio = ejercicio.Ejercicio.Id,
                            NombreEjercicio = ejercicio.Ejercicio.Nombre,
                            Segundos = ejercicio.Segundos,
                            SegundosDescanso = ejercicio.SegundosDescanso,
                            Repeticiones = ejercicio.Repeticiones,
                            RepeticionesAvanzado = ejercicio.RepeticionesAvanzado,
                            Bloque = ejercicio.Bloque,
                            Orden = ejercicio.Orden
                        });
                    }
            }

            return r;
        }

        #endregion

    }
}
