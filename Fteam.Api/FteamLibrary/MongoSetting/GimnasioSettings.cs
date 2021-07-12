using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.MongoSetting
{
    public class GimnasioSettings : Interfaces.ISettingsMDB
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string GimnasioCollection { get; set; }
        public string UsuarioCollection { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

    }
}
