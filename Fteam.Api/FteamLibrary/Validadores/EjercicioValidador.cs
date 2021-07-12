using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Validadores
{
    public class EjercicioValidador
    {
        private StringBuilder _error = new StringBuilder();
        public string Error
        {
            get
            {
                return _error.ToString();
            }
        }



        public bool Validar(Entities.Ejercicio ejercicio)
        {
            if (String.IsNullOrWhiteSpace(ejercicio.Nombre))
            {
                _error.AppendLine("Nombre es un dato obligatorio");
            }

            if (string.IsNullOrWhiteSpace(ejercicio.Detalle))
            {
                _error.AppendLine("Debe indicar el archivo del ejercicio");
            }

            if (!string.IsNullOrWhiteSpace(_error.ToString()))
            {
                return false;

            }

            return true;
        }
    }
}
