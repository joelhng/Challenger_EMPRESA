﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace FteamLibrary.Entities
{

    /// <summary>
    /// Plan de entrenamiento para una persona en particular
    /// </summary>
    public class PlanEntrenamiento
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Detalle { get; set; }
        [BsonElement]
        public int EntrenamientosSemanales { get; set; }      
        [BsonElement]
        public List<Rutina> Rutina { get; set; }
    }
}
