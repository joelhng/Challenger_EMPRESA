using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FteamApi.Models
{
    public class UsuarioInfo
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int MinutosSesion { get; set; }
        public string Detalle { get; set; }
        public eTipoUsuario Nivel { get; set; }
        public string Nombre { get; set; }
        public string GimnasioId { get; set; }
        public string GimnasioDenominacion { get; set; }

    }
}
