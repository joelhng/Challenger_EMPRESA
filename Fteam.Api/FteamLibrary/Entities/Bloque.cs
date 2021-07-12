using FteamLibrary.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamLibrary.Entities
{
    public class Bloque
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Nombre { get; set; }
        [BsonElement]
        public int Duracion { get; set; }

        [BsonElement]
        public int RepeticionBloque { get; set; }

        [BsonElement]
        public int Orden { get; set; }

        [BsonElement]
        public eTipoBloque TipoBloque { get; set; }
        [BsonElement]
        public List<EjercicioActividad> EjercicioActividad { get; set; } = new List<EjercicioActividad>();

        /// <summary>
        /// Obsoleto
        /// </summary>
        [BsonElement]
        public List<EjercicioActividad> EjercicioActividades { get; set; } = new List<EjercicioActividad>();

    }
}
