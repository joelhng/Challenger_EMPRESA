using FteamLibrary.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{

    /// <summary>
    /// Horarios disponbile para la semana de entrenamiento
    /// </summary>
    public class Horario
    {      

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Descripcion { get; set; }
        [BsonElement]
        public eDia Dia { get; set; }
        [BsonElement]
        public int Hora { get; set; }
        [BsonElement]
        public int Limite { get; set; }
        [BsonElement]
        public eTipoClase TipoClase { get; set; }        
        
    }
}
