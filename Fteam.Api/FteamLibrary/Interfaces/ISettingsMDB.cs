using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Interfaces
{
    public interface ISettingsMDB
    {
        string Server { get; set; }
        string Database { get; set; }
        string GimnasioCollection { get; set; }
        string UsuarioCollection { get; set; }
        string Usuario { get; set; }
        string Contrasena { get; set; }
    }
}
