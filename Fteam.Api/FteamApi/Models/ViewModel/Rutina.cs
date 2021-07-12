using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    /// <summary>
    /// Rutina que muestra la información como grilla
    /// </summary>
    public class Rutina
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Detalle { get; set; }

        public int TipoClase { get; set; }

        public List<EjercicioActividad> EjercicioActividades { get; set; } = new List<EjercicioActividad>();

    }
}
