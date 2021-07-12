using FteamLibrary.Entities;
using FteamLibrary.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FteamLibrary.Validadores;
using MongoDB.Bson;

namespace FteamLibrary.Services
{
    public class EjercicioService
    {
        private IMongoCollection<Gimnasio> _gimnasio;
        Validadores.EjercicioValidador _validador = new Validadores.EjercicioValidador();

       

        public EjercicioService(ISettingsMDB settingsMDB)
        {
            _gimnasio = MongoSetting.SettingsUtil.GetDb(settingsMDB).GetCollection<Gimnasio>(settingsMDB.GimnasioCollection);
        }

        /// <summary>
        /// Retorna todos los ejercicios cargados por un gimnasio
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Ejercicio> GetxGimnasio(string IdGimnasio)
        {
            return _gimnasio.Find(x => x.Id == IdGimnasio).First().Ejercicios;
        }

        public Ejercicio Get(string IdGimnasio, string Id)
        {
            //Utilizo sub filtros de mongo. Así se debería consulta en otro lugares cuando quiero buscar por sub-documentos
            var builder = Builders<Ejercicio>.Filter;
            var filter = builder.Eq(x => x.Id, Id);
            var fg = Builders<Gimnasio>.Filter.ElemMatch(x => x.Ejercicios, filter);


            return _gimnasio.Find(x => x.Id == IdGimnasio).First().Ejercicios.Where(y => y.Id == Id).First();
        }

        public async void Insert(string idGimnasio, Ejercicio ejercicio)
        {

            if (!_validador.Validar(ejercicio))
            {
                throw new Exception(_validador.Error.ToString());
            }

            var g = _gimnasio.Find(x => x.Id == idGimnasio).First();

            ejercicio.Id = ObjectId.GenerateNewId().ToString();

            var builder = Builders<Gimnasio>.Filter;
            var filter = builder.Eq(x => x.Id, idGimnasio);
            var update = Builders<Gimnasio>.Update
                         .AddToSet(x => x.Ejercicios, ejercicio);

            var updateResult = await _gimnasio.UpdateOneAsync(filter, update);
        }

        public async void Update(string idGimnasio, Ejercicio ejercicio)
        {
            if (!_validador.Validar(ejercicio))
            {
                throw new Exception(_validador.Error.ToString());
            }

            var update = Builders<Gimnasio>.Update.Set(x=> x.Ejercicios[-1].Nombre, ejercicio.Nombre)
                                                  .Set(x => x.Ejercicios[-1].Detalle, ejercicio.Detalle)
                                                  .Set(x => x.Ejercicios[-1].Archivo, ejercicio.Archivo)
                                                  .Set(x => x.Ejercicios[-1].Url, ejercicio.Url);
            
            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("Ejercicios", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse( ejercicio.Id)) })))
                            });

            var e = _gimnasio.Find(filter).ToList();

            var updateResult = await _gimnasio.UpdateOneAsync(filter, update);

        }
    }
}
