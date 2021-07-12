using FteamApi.Api.Atributos;
using FteamApi.Models.ViewModel;
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
    public class EjerciciosController : ControllerBase
    {
        private EjercicioService _ejercicioService;

        public EjerciciosController(EjercicioService ejercicioService)
        {
            _ejercicioService = ejercicioService;
        }

        // GET: api/ejercicio/5
        [HttpGet("{id}")]
        [Authorize]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<IEnumerable<Models.ViewModel.Ejercicio>> Get(string id)
        {

            var e = _ejercicioService.GetxGimnasio(id);

            List<Models.ViewModel.Ejercicio> modelos = new List<Ejercicio>();


            foreach (var item in e)
            {
                modelos.Add(Fill(item));

            }

            return modelos;
        }

        private Models.ViewModel.Ejercicio Fill(FteamLibrary.Entities.Ejercicio ejercicio)
        {
            return new Models.ViewModel.Ejercicio() { Id = ejercicio.Id, Nombre = ejercicio.Nombre, Detalle = ejercicio.Detalle, Url = ejercicio.Url, Archivo = ejercicio.Archivo };
        }


    }
}
