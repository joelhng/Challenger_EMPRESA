using FteamLibrary.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamLibrary.Entities
{
    public class CertificadoPreventivo
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public DateTime Fecha { get; set; }

        [BsonElement]
        public bool TieneFiebre { get; set; }

        [BsonElement]
        public bool TieneEstadoGripal { get; set; }

        [BsonElement]
        public bool TieneCansancioCorporal { get; set; }

        [BsonElement]
        public bool FaltanteGustoOlfato { get; set; }

        [BsonElement]
        public bool ContactoConSintomatico { get; set; }

        [BsonElement]
        public string Observaciones { get; set; }

    }
}
