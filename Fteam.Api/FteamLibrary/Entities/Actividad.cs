using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{

    /// <summary>
    /// Entrenamiento realizado por una persona
    /// </summary>
    public class Actividad
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public DateTime Fecha { get; set; }
        [BsonElement]
        public eTipoClase TipoClase { get; set; }

       
     }
}
