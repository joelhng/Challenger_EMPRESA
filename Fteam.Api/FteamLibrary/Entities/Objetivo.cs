using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{
    /// <summary>
    /// Catalogo de objetivos de entrenamiento
    /// Opc: Bajar de peso, Aumentar masa muscular, Mantenimiento, Clases semanales
    /// </summary>
    public class Objetivo
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Descripcion { get; set; }
        [BsonElement]
        public eObjetivo eObjetivo { get; set; }
        [BsonElement]
        public PlanEntrenamiento PlanEntrenamiento { get; set; } = new PlanEntrenamiento();
    }
}
