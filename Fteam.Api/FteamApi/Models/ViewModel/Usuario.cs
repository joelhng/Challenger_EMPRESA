using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models.ViewModel
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
        public bool Bloqueado { get; set; }
        public eGenero Genero { get; set; }
        public string Identificacion { get; set; }
        public eTipoUsuario Nivel { get; set; }
       
        public string IdGimnasio { get; set; }
        public double Peso { get; set; }

    }
}
