using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{
    /// <summary>
    /// Certificado médico de la persona
    /// </summary>
    public class Certificado
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public DateTime Vencimiento { get; set; }
        [BsonElement]
        public string Archivo { get; set; }                
  
       
        public byte[] ArchivoFisico { get; set; }

    }
}
