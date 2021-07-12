using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    public class Bloque
    {        
        public string Id { get; set; }        
        public string Nombre { get; set; }        
        public int Duracion { get; set; }
        public int RepeticionBloque { get; set; }

        public int Orden { get; set; }
        
        public eTipoBloque TipoBloque { get; set; }
        
        public List<EjercicioActividad> EjercicioActividad { get; set; } = new List<EjercicioActividad>();


    }
}
