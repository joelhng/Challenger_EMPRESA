using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FteamLibrary.Entities
{
    public class Gimnasio
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Codigo { get; set; }
        [BsonElement]
        public string Correo { get; set; }
        [BsonElement]
        public string Denominacion { get; set; }
        [BsonElement]
        public bool Bloqueado { get; set; }
        [BsonElement]
        public string Logo { get; set; }
        [BsonElement]
        public List<PlanEntrenamiento> PlanesEntrenamiento { get; set; } = new List<PlanEntrenamiento>();

        [BsonElement]
        public List<Ejercicio> Ejercicios { get; set; } = new List<Ejercicio>();

        [BsonElement]
        public List<Rutina> Rutinas { get; set; } = new List<Rutina>();

        [BsonElement]
        public List<HorariosDisponibles> HorariosDisponibles { get; set; } = new List<HorariosDisponibles>();


        [NotMapped]
        public byte[] ArchivoFisico { get; set; }

    }
}
