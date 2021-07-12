using FteamApi.Api.Atributos;
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
    public class HorariosController : ControllerBase
    {

        HorariosService _horarioService;
        GimnasioService _gimnasioService;

        public HorariosController(HorariosService hs, GimnasioService gs)
        {
            _horarioService = hs;
            _gimnasioService = gs;

        }

        [HttpGet]
        [AllowAnonymous]
        public List<HorariosDisponibles> Get()
        {

            var idGimnasio = "";

            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;


            if (identity.FindFirst("IdGimnasio") != null)
            {
                idGimnasio = identity.FindFirst("IdGimnasio").Value;
            }

            if (String.IsNullOrWhiteSpace(idGimnasio))
            {
                idGimnasio = _gimnasioService.Get().First().Id;
            }

            return _horarioService.Get(idGimnasio);
        }

        // GET: api/ejercicio/5
        [HttpGet("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public HorariosDisponibles Get(string id)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;


            var r = _horarioService.Get(idGimnasio, id);

            return r;
        }

        [HttpPost]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[2] { eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task PostUsuario([FromBody] HorariosDisponibles horario)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            await _horarioService.InsertAsync(idGimnasio, horario);
        }


        // PUT api/<RutinaController>/5
        [HttpPut]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public async Task PutAsync([FromBody] HorariosDisponibles horario)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            await _horarioService.UpdateAsync(idGimnasio, horario);
        }

        [HttpDelete("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public void Delete(string id)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            _horarioService.Remove(idGimnasio, id);
        }


    }
}
