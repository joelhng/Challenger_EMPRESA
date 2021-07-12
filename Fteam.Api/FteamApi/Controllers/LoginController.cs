using FteamApi.Models;
using FteamLibrary.Entities;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FteamApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        public UsuarioService _usuarioService;
        public GimnasioService _gimnasioService;
        private IConfiguration _config;

        public LoginController(IConfiguration configuration, UsuarioService usuarioService, GimnasioService gimnasioService)
        {
            _config = configuration;
            _usuarioService = usuarioService;
            _gimnasioService = gimnasioService;
        }
        
        [HttpPost]
        public ActionResult<UsuarioInfo> Login([FromBody] UsuarioLogin userInfo)
        {
            UsuarioInfo userToken = new UsuarioInfo();

            var userData = AuthenticateUser(userInfo);

            if (userData != null)
            {
                if (userData.Bloqueado)
                {
                    return BadRequest("El usuario se encuentra bloqueado");
                }

                if (userData.Contrasena != userInfo.Contrasena)
                {
                    userToken.Expiration = DateTime.Now;
                    userToken.Detalle = "Verifique la información enviada";

                    return BadRequest(userToken);
                }

                userToken = GenerateJSONWebToken(userData);
            }
            else
            {
                userToken.Expiration = DateTime.Now;
                userToken.Detalle = "Verifique la información enviada";

                return BadRequest(userToken);
            }
          

            return userToken;
        }


        #region privado 

        private Usuario AuthenticateUser(UsuarioLogin userInfo)
        {
            if (String.IsNullOrWhiteSpace(userInfo.Correo) || string.IsNullOrWhiteSpace(userInfo.Contrasena))
            {
                return null;
            }

            var usuario = _usuarioService.GetxCorreo(userInfo.Correo);

            return usuario;
        }

        private UsuarioInfo GenerateJSONWebToken(Usuario usuario)
        {
            var userToken = new UsuarioInfo();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[] {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.GivenName, usuario.Nombre),
                new Claim("IdGimnasio", usuario.IdGimnasio),
                new Claim("IdUsuario", usuario.Id.ToString()),
                new Claim("Nivel", usuario.Nivel.ToString())
            };

            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials);


            var g = _gimnasioService.Get(usuario.IdGimnasio);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            userToken.Id = usuario.Id;
            userToken.MinutosSesion = 180;
            userToken.Expiration = DateTime.Now.AddMinutes(180);
            userToken.Token = encodetoken;
            userToken.Nivel = usuario.Nivel;
            userToken.Nombre = usuario.Nombre;
            userToken.GimnasioId = usuario.IdGimnasio;
            userToken.GimnasioDenominacion = g.Denominacion;

            return userToken;
        }


        #endregion
    }
}
