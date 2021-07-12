using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Entities
{
    public class Rutina
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string Nombre { get; set; }

        [BsonElement]
        public string Detalle { get; set; }

        [BsonElement]
        public List<Bloque> Bloques { get; set; } = new List<Bloque>();

        [BsonElement]
        public eTipoClase TipoClase { get; set; }

    }
}
