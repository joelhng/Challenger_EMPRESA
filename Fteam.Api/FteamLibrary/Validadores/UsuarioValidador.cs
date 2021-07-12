using FteamLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Validadores
{
    public class UsuarioValidador
    {

        public StringBuilder Error { get; set; } = new StringBuilder();

        public bool Validacion(Usuario usuario)
        {

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                Error.AppendLine("Nombre es un dato obligatorio");
            }

            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Error.AppendLine("Correo es un dato obligatorio");
            }

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                Error.Append("Contraseña es un dato obligatorio");
            }

            if (string.IsNullOrWhiteSpace(usuario.Telefono))
            {
                Error.Append("Telefono es un dato obligatorio");
            }

            if (usuario.FechaNacimiento == new DateTime())
            {
                Error.Append("Fecha de nacimiento es un dato obligatorio");
            }

            if (String.IsNullOrWhiteSpace(Error.ToString()))
            {
                return false;
            }

            return true;
        }


    }
}
