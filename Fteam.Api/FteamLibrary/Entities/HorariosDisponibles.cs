using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace FteamLibrary.Entities
{
    public class HorariosDisponibles
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Clase { get; set; }
        [BsonElement]
        public string CuposLibres { get; set; }
        [BsonElement]
        public string DiaHorario { get; set; }

    }
}
