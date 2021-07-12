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
    public class InitController : ControllerBase
    {

        private UsuarioService _usuarioService;
        private GimnasioService _gimnasioService;        

        public InitController(UsuarioService usuarioService,
                                GimnasioService gimnasioService)
        {           

            _usuarioService = usuarioService;
            _gimnasioService = gimnasioService;

        }



        [HttpGet]
        public string Get()
        {
           
            try
            {
                //Creo el gimnasio principal F+ y el usuario superadmin

                FteamLibrary.Entities.Gimnasio gimnasio = new FteamLibrary.Entities.Gimnasio();

                gimnasio.Bloqueado = false;
                gimnasio.Codigo = "F+";
                gimnasio.Correo = "joelhng@gmail.com";
                gimnasio.Denominacion = "F+";

                _gimnasioService.Insert(gimnasio);

                FteamLibrary.Entities.Usuario usr = new FteamLibrary.Entities.Usuario();

                usr.Correo = "joelhng@gmail.com";
                usr.Nombre = "Joel Navarrete";
                usr.Identificacion = "34806747";
                usr.Contrasena = "123456";
                usr.FechaNacimiento = new DateTime(1989, 06, 10);
                usr.Nivel = FteamLibrary.Enum.eTipoUsuario.SuperUsuario;
                usr.Telefono = "11 6651 9929";
                usr.Genero = FteamLibrary.Enum.eGenero.Hombre;
                usr.IdGimnasio = _gimnasioService.Get().First().Id;
                usr.Peso = 100;


                _usuarioService.Insert(usr);
            }
            catch 
            {            
            }

            try
            {
                FteamLibrary.Entities.Usuario usr = new FteamLibrary.Entities.Usuario();

                usr.Correo = "fede.sanchezpf@gmail.com";
                usr.Nombre = "Federico Sanchez";
                usr.Identificacion = "########";
                usr.Contrasena = "123456";
                usr.FechaNacimiento = new DateTime(1992, 12, 19);
                usr.Nivel = FteamLibrary.Enum.eTipoUsuario.Administrador;
                usr.Telefono = "1137932984";
                usr.Genero = FteamLibrary.Enum.eGenero.Hombre;
                usr.IdGimnasio = _gimnasioService.Get().First().Id;
                usr.Peso = 70;


                _usuarioService.Insert(usr);
            }
            catch 
            {

                throw;
            }

            return "Inicialización completada";
        }


    }
}
