using FteamApi.Api.Atributos;
using FteamLibrary.Entities;
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
    public class GimnasioController : ControllerBase
    {

        public GimnasioService _gimnasioService;

        public GimnasioController(GimnasioService gimnasioService)
        {
            _gimnasioService = gimnasioService;

        }

        [HttpGet]
        public ActionResult<List<Gimnasio>> Get()
        {

            return _gimnasioService.Get();
        }

        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[1] { eTipoUsuario.SuperUsuario})]
        [HttpPost]
        public ActionResult<Gimnasio> Post(Gimnasio gimnasio)
        {

            _gimnasioService.Insert(gimnasio);

            return Ok(gimnasio);
        }

    }
}
