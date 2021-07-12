using FteamApi.Api.Atributos;
using FteamApi.Models.ViewModel;
using FteamLibrary.Entities;
using FteamLibrary.Enum;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutinasController : ControllerBase
    {

        RutinaService _rutinaService;
        UsuarioService _usuarioService;

        public RutinasController(RutinaService rutinaService, UsuarioService usuarioService)
        {
            _rutinaService = rutinaService;
            _usuarioService = usuarioService;
        }

        // GET: api/ejercicio/5/1
        [HttpGet("{tipoRutina}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<IEnumerable<Models.ViewModel.Rutina>> Get(int tipoRutina = 0)
        {
            List<Models.ViewModel.Rutina> modelos = new List<Models.ViewModel.Rutina>();

            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;            
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;


            var r = _rutinaService.Get(idGimnasio);

            foreach (var item in r.Where(x=> x.TipoClase == (eTipoClase) tipoRutina  || x.TipoClase == eTipoClase.Libre))
            {
                modelos.Add(Fill(item));
            }

            return modelos;
        }



        [HttpPost]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task PostAsync([FromBody] RutinaUsuario rutinaUsuario)
        {
            try
            {

                var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

                var idGimnasio = identity.FindFirst("IdGimnasio").Value;

                if (rutinaUsuario.Asignado)
                {
                    await _rutinaService.RefreshRutina(idGimnasio, rutinaUsuario.IdRutina);
                }

                _usuarioService.SetRutinaUsuario(idGimnasio, rutinaUsuario.IdUsuario, rutinaUsuario.IdRutina, rutinaUsuario.Asignado);

            }
            catch
            {
            }
        }


        // GET: api/rutina/5/usuarios
        [HttpGet("{idRutina}/usuarios")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<IEnumerable<Models.ViewModel.RutinaUsuario>> GetUsuarios(string idRutina)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            var usuarios = _usuarioService.GetxGimnasio(idGimnasio);


            List<Models.ViewModel.RutinaUsuario> rutinas = new List<Models.ViewModel.RutinaUsuario>();

            foreach (var item in usuarios)
            {

                if (item.Rutinas.Any(x => x.Id == idRutina))
                {
                    rutinas.Add(new Models.ViewModel.RutinaUsuario()
                    {
                        IdUsuario = item.Id,
                        Nombre = item.Nombre,
                        IdRutina = idRutina,
                        Asignado = true
                    });

                }
                else
                {
                    rutinas.Add(new Models.ViewModel.RutinaUsuario()
                    {
                        IdUsuario = item.Id,
                        Nombre = item.Nombre,
                        IdRutina = idRutina,
                        Asignado = false
                    });
                }
            }


            return rutinas;
        }




        public Models.ViewModel.Rutina Fill(FteamLibrary.Entities.Rutina rutina)
        {
            Models.ViewModel.Rutina r = new Models.ViewModel.Rutina();

            r.Id = rutina.Id;
            r.Nombre = rutina.Nombre;
            r.Detalle = rutina.Detalle;            
            r.TipoClase = (int)rutina.TipoClase;

            foreach (var item in rutina.Bloques.OrderBy(x => x.Orden))
            {

                r.EjercicioActividades.Add(new Models.ViewModel.EjercicioActividad()
                {
                    Id = item.Id,
                    Orden = item.Orden,
                    NombreEjercicio = item.Nombre,
                    Duracion = item.Duracion,
                    TipoBloque = item.TipoBloque
                });

                if (item.EjercicioActividad != null)
                    foreach (var ejercicio in item.EjercicioActividad)
                    {
                        r.EjercicioActividades.Add(new Models.ViewModel.EjercicioActividad()
                        {
                            IdEjercicio = ejercicio.Ejercicio.Id,
                            NombreEjercicio = ejercicio.Ejercicio.Nombre,
                            Segundos = ejercicio.Segundos,
                            Repeticiones = ejercicio.Repeticiones,
                            Bloque = ejercicio.Bloque
                        });

                    }


            }

            return r;
        }


    }
}
