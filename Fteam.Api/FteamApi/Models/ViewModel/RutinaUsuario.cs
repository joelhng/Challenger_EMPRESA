using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    public class RutinaUsuario
    {
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string IdRutina { get; set; }
        public bool Asignado { get; set; }
    }
}
