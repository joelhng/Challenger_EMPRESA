using FteamApi.Api.Atributos;
using FteamLibrary.Enum;
using FteamLibrary.Services;
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
    public class CertificadoPrevController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public CertificadoPrevController(UsuarioService usuarioService, RutinaService rutinaService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<FteamLibrary.Entities.CertificadoPreventivo> GetCertificadoPreventivo()
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var id = identity.FindFirst("IdUsuario").Value;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;


            var user = _usuarioService.Get(id);

            if (user.CertificadoPreventivo.Fecha == new DateTime()) user.CertificadoPreventivo.Fecha = DateTime.Now;


            return user.CertificadoPreventivo;
        }


        [HttpPost]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<FteamLibrary.Entities.CertificadoPreventivo> PostCertificadoPreventivo([FromBody] FteamLibrary.Entities.CertificadoPreventivo certificadoPreventivo)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            var id = identity.FindFirst("IdUsuario").Value;
            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            _usuarioService.UpdateCertificado(id, certificadoPreventivo);

            return certificadoPreventivo;
        }



    }
}
