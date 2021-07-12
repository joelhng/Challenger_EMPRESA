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
    public class UsuariosController : ControllerBase
    {

        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.Profesor, eTipoUsuario.Nutricionista, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<IEnumerable<FteamLibrary.Entities.Usuario>> Usuarios(string id)
        {

            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

            var idGimnasio = identity.FindFirst("IdGimnasio").Value;

            var usuarios = _usuarioService.GetxGimnasio(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

    }
}
