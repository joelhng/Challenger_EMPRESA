using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{
    /// <summary>
    /// Pago de usuario 
    /// </summary>
    public class Pago
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public DateTime FechaPago { get; set; } = new DateTime();
        [BsonElement]
        public double Monto { get; set; }
        [BsonElement]
        public short? CantClases { get; set; }

    }
}
