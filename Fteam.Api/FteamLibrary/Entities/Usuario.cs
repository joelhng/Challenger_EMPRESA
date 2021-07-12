using FteamLibrary.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.Entities
{
    public class Usuario
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        [BsonElement]
        public string Correo { get; set; }
        [BsonElement]
        public string Telefono { get; set; }
        [BsonElement]
        public DateTime FechaNacimiento { get; set; }
        [BsonElement]
        public string Contrasena { get; set; }
        [BsonElement]
        public bool Bloqueado { get; set; }
        [BsonElement]
        public eGenero Genero { get; set; }
        [BsonElement]
        public string Identificacion { get; set; }
        [BsonElement]
        public double Peso { get; set; }

        [BsonElement]
        public eTipoUsuario Nivel { get; set; }
        [BsonElement]
        public Objetivo Objetivo { get; set; } = new Objetivo();

        [BsonElement]
        public string IdGimnasio { get; set; }

        [BsonElement]
        public List<Rutina> Rutinas { get; set; } = new List<Rutina>();

        [BsonElement]
        public CertificadoPreventivo CertificadoPreventivo { get; set; } = new CertificadoPreventivo();


    }
}
