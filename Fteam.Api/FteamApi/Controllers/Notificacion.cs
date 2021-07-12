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
    public class Notificacion : ControllerBase
    {
        //[HttpGet]
        //[AllowAnonymous]
        //public List<HorariosDisponibles> Get()
        //{

        //    var idGimnasio = "";

        //    var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;


        //    if (identity.FindFirst("IdGimnasio") != null)
        //    {
        //        idGimnasio = identity.FindFirst("IdGimnasio").Value;
        //    }

        //    if (String.IsNullOrWhiteSpace(idGimnasio))
        //    {
        //        idGimnasio = _gimnasioService.Get().First().Id;
        //    }

        //    return _horarioService.Get(idGimnasio);
        //}
    }
}
