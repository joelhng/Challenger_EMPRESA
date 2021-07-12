using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    public class Ejercicio
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Archivo { get; set; }
        public string Detalle { get; set; }
     

    }
}
