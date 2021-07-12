using FteamLibrary.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Entities
{
    /// <summary>
    /// Ejercicio en rutina
    /// </summary>
    public class EjercicioActividad
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public int Bloque { get; set; }

        [BsonElement]
        public int Orden { get; set; }

        [BsonElement]
        public int Repeticiones { get; set; }

        [BsonElement]
        public int RepeticionesAvanzado { get; set; }

        [BsonElement]
        public int Segundos { get; set; }

        [BsonElement]
        public int SegundosDescanso { get; set; }

        [BsonElement]
        public Ejercicio Ejercicio { get; set; } = new Ejercicio();

    }
}
