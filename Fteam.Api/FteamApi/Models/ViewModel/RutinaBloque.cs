using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    /// <summary>
    /// Rutina que retorna la información con división por bloques
    /// </summary>
    public class RutinaBloque
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Detalle { get; set; }

        public List<Bloque> Bloques { get; set; } = new List<Bloque>();

    }
}
