using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    public class EjercicioActividad
    {
        public string Id { get; set; }
        public int Orden { get; set; }
        public int Bloque { get; set; }
        
        public int Repeticiones { get; set; }
        public int RepeticionesAvanzado { get; set; }


        public int Segundos { get; set; }
        public int SegundosDescanso { get; set; }

        //TODO: Quitar y utiliza propiedad Ejercicio
        public string IdEjercicio { get; set; }
        //TODO: Quitar y utiliza propiedad Ejercicio
        public string NombreEjercicio { get; set; }

        //Pasar a objecto Bloque
        public bool Cabecera { get; set; }
        //Pasar a objecto Bloque
        public eTipoBloque TipoBloque { get; set; }
        //Pasar a objecto Bloque
        public int Duracion { get; set; }
        public int RepeticionBloque { get; set; }        

        public Ejercicio Ejercicio { get; set; } = new Ejercicio();


    }
}
