using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Entities
{
    public class Ejercicio
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Nombre { get; set; }

        [BsonElement]
        public string Url { get; set; }

        [BsonElement]
        public string Archivo { get; set; }

        [BsonElement]
        public string Detalle { get; set; }
    }
}
