using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FteamLibrary.Entities
{

    /// <summary>
    /// Tipo de clase.
    /// Opc: Funcional, Hit, GAP, Zumba, Crossfit
    /// </summary>
    public enum eTipoClase
    {
        Libre = 0,
        Funcional = 1,
        Personalizado = 2,
        GAP = 3,
        Zumba = 4,
        Crossfit = 5
    }
}
