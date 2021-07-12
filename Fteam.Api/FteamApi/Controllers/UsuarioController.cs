using FteamApi.Api.Atributos;
using FteamApi.Models.ViewModel;
using FteamLibrary.Enum;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        private UsuarioService _usuarioService;
        private RutinaService _rutinaService;

        public UsuarioController(UsuarioService usuarioService, RutinaService rutinaService)
        {
            _usuarioService = usuarioService;
            _rutinaService = rutinaService;
        }

        [HttpGet("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<Models.ViewModel.Usuario> GetUsuario(string id)
        {
            var user = _usuarioService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            var u = Fill(user);

            return u;
        }

        [HttpGet("getinfousuario/{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<FteamLibrary.Entities.Usuario> GetInfoUsuario(string id)
        {
            var user = _usuarioService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
          
            return user;
        }

        [HttpGet("getrutina/{id}")]
        [HttpGet("getrutina")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task<ActionResult<IEnumerable<RutinaBloque>>> GetRutinaAsync(string idRutina = null)
        {
            List<Models.ViewModel.RutinaBloque> rutina;

            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var id = identity.FindFirst("IdUsuario").Value;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;


            var user = _usuarioService.Get(id);

            if (!String.IsNullOrWhiteSpace(idRutina))
            {
                await _rutinaService.RefreshRutina(idGimnasio, idRutina);
            }

            foreach (var item in user.Rutinas)
            {
                await _rutinaService.RefreshRutina(idGimnasio, item.Id);

                _usuarioService.SetRutinaUsuario(idGimnasio, id, item.Id, false);
                _usuarioService.SetRutinaUsuario(idGimnasio, id, item.Id, true);
            }

            user = _usuarioService.Get(id);

            rutina = Fill(user.Rutinas.Where(x => String.IsNullOrWhiteSpace(idRutina) || x.Id == idRutina).ToList());

            return rutina;
        }


        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[2] { eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<FteamLibrary.Entities.Usuario> PostUsuario([FromBody] Models.ViewModel.Usuario usuario)
        {
            FteamLibrary.Entities.Usuario u = new FteamLibrary.Entities.Usuario();


            try
            {

                usuario.Bloqueado = false;
                u = Fill(usuario);

                _usuarioService.Insert(u);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return u;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] {eTipoUsuario.SinPrivilegios, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<FteamLibrary.Entities.Usuario> PutUsuario(string id, [FromBody] Models.ViewModel.Usuario usuario)
        {
            FteamLibrary.Entities.Usuario u = new FteamLibrary.Entities.Usuario();

            u = Fill(usuario);

            _usuarioService.Update(u);

            return u;
        }



        private FteamLibrary.Entities.Usuario Fill(Models.ViewModel.Usuario usuario)
        {
            FteamLibrary.Entities.Usuario u = new FteamLibrary.Entities.Usuario();

            u.Id = usuario.Id;
            u.Correo = usuario.Correo;
            u.Nombre = usuario.Nombre;
            u.Bloqueado = usuario.Bloqueado;
            u.Contrasena = usuario.Contrasena;
            u.IdGimnasio = usuario.IdGimnasio;
            u.Genero = usuario.Genero;
            u.Telefono = usuario.Telefono;
            u.Nivel = usuario.Nivel;
            u.Identificacion = usuario.Identificacion;
            u.FechaNacimiento = usuario.FechaNacimiento;

            return u;
        }

        private Models.ViewModel.Usuario Fill(FteamLibrary.Entities.Usuario usuario)
        {
            Models.ViewModel.Usuario u = new Models.ViewModel.Usuario();

            u.Id = usuario.Id;
            u.Nombre = usuario.Nombre;
            u.Bloqueado = usuario.Bloqueado;
            u.Contrasena = usuario.Contrasena;
            u.IdGimnasio = usuario.IdGimnasio;
            u.Genero = usuario.Genero;
            u.Telefono = usuario.Telefono;
            u.Nivel = usuario.Nivel;
            u.Identificacion = usuario.Identificacion;
            u.FechaNacimiento = usuario.FechaNacimiento;
            u.Correo = usuario.Correo;
            u.Peso = usuario.Peso;

            return u;
        }

        private List<Models.ViewModel.RutinaBloque> Fill(List<FteamLibrary.Entities.Rutina> rutina)
        {

            var vmrutina = new List<Models.ViewModel.RutinaBloque>();

            foreach (var item in rutina)
            {
                Models.ViewModel.RutinaBloque r = new Models.ViewModel.RutinaBloque();

                r.Id = item.Id;
                r.Detalle = item.Detalle;
                r.Nombre = item.Nombre;


                item.Bloques.ForEach(x =>
                {
                    Bloque b = new Bloque();
                    b.Id = x.Id;
                    b.Duracion = x.Duracion;
                    b.Nombre = x.Nombre;
                    b.Orden = x.Orden;
                    b.TipoBloque = x.TipoBloque;
                    b.RepeticionBloque = x.RepeticionBloque;


                    x.EjercicioActividad.ForEach(y =>
                    {
                        EjercicioActividad ea = new EjercicioActividad();

                        ea.Id = y.Id;
                        ea.Bloque = y.Bloque;
                        ea.Cabecera = true;
                        ea.Orden = y.Orden;
                        ea.Repeticiones = y.Repeticiones;
                        ea.Segundos = y.Segundos;
                        ea.SegundosDescanso = y.SegundosDescanso;
                        ea.IdEjercicio = y.Ejercicio.Id;
                        ea.NombreEjercicio = y.Ejercicio.Nombre;


                        ea.Ejercicio.Id = y.Ejercicio.Id;
                        ea.Ejercicio.Nombre = y.Ejercicio.Nombre;
                        ea.Ejercicio.Url = y.Ejercicio.Url;
                        ea.Ejercicio.Archivo = y.Ejercicio.Archivo;
                        ea.Ejercicio.Detalle = y.Ejercicio.Detalle;

                        b.EjercicioActividad.Add(ea);
                    });


                    r.Bloques.Add(b);
                });
                vmrutina.Add(r);
            }

            return vmrutina;
        }


    }
}
