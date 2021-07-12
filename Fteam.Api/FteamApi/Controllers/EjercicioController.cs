using FteamApi.Api.Atributos;
using FteamLibrary.Entities;
using FteamLibrary.Enum;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FteamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjercicioController : ControllerBase
    {
        private EjercicioService _ejercicioService;
        private IWebHostEnvironment _hostEnvironment;

        public EjercicioController(EjercicioService ejercicioService, IWebHostEnvironment environment)
        {
            _ejercicioService = ejercicioService;
            _hostEnvironment = environment;
        }

        // GET: api/ejercicio/5/1
        [HttpGet("{idGimnasio}/{id}")]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[4] { eTipoUsuario.SinPrivilegios, eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]        
        public ActionResult<Models.ViewModel.Ejercicio> Get(string idGimnasio, string id)
        {
            var identity = Request.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

            var idGim = identity.FindFirst("IdGimnasio").Value;

            var e = _ejercicioService.Get(idGim, id);
            return Fill(e);
        }

        [HttpPost("{idGimnasio}")]        
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<Models.ViewModel.Ejercicio> Post(string idGimnasio, [FromBody] Models.ViewModel.Ejercicio ejercicio)
        {
            var e = Fill(ejercicio);

            try
            {

                 _ejercicioService.Insert(idGimnasio, e);

                if (!String.IsNullOrWhiteSpace(ejercicio.Archivo))
                {
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    string sourceFile = System.IO.Path.Combine(Path.GetTempPath(), ejercicio.Archivo);
                    string destFile = System.IO.Path.Combine(pathToSave, $"{e.Id}.{ Path.GetExtension(ejercicio.Archivo)}");

                    System.IO.File.Copy(sourceFile, destFile, true);
                }

            }
            catch (Exception Ex)
            {
                BadRequest(Ex.Message);
            }

            return Fill(e);
        }

        [HttpPut("{idGimnasio}")]
        [Authorize]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public ActionResult<Models.ViewModel.Ejercicio> Put(string idGimnasio, [FromBody] Models.ViewModel.Ejercicio ejercicio)
        {

            try
            {

                var e = Fill(ejercicio);

                _ejercicioService.Update(idGimnasio, e);

                if (!String.IsNullOrWhiteSpace(ejercicio.Archivo))
                {
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    string sourceFile = System.IO.Path.Combine(Path.GetTempPath(), ejercicio.Archivo);
                    string destFile = System.IO.Path.Combine(pathToSave, e.Id);

                    System.IO.File.Copy(sourceFile, destFile, true);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ejercicio;
        }

        private Ejercicio Fill(Models.ViewModel.Ejercicio ejercicio)
        {

            return new Ejercicio() { Id = ejercicio.Id, Nombre = ejercicio.Nombre, Detalle = ejercicio.Detalle, Url = ejercicio.Url, Archivo = ejercicio.Archivo };
        }

        private Models.ViewModel.Ejercicio Fill(Ejercicio ejercicio)
        {
            return new Models.ViewModel.Ejercicio() { Id = ejercicio.Id, Nombre = ejercicio.Nombre, Detalle = ejercicio.Detalle, Url = ejercicio.Url, Archivo = ejercicio.Archivo };
        }


        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        [AuthorizeLevel(TipoUsuarios = new eTipoUsuario[3] { eTipoUsuario.Profesor, eTipoUsuario.SuperUsuario, eTipoUsuario.Administrador })]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];


                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    //var fullPath = Path.Combine(pathToSave, fileName);
                    var fullPath = Path.Combine(Path.GetTempPath(), fileName);
                    var dbPath = Path.Combine(Path.GetTempPath(), fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
