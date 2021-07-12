using FteamLibrary.Entities;
using FteamLibrary.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FteamLibrary.Services
{
    public class RutinaService
    {
        private IMongoCollection<Gimnasio> _gimnasio;
        private EjercicioService _ejercicioService;
        Validadores.RutinaValidador _rutinaValidador = new Validadores.RutinaValidador();

        public RutinaService(ISettingsMDB settingsMDB)
        {
            _gimnasio = MongoSetting.SettingsUtil.GetDb(settingsMDB).GetCollection<Gimnasio>(settingsMDB.GimnasioCollection);
            _ejercicioService = new  EjercicioService(settingsMDB);
        }


        public List<Rutina> Get(string IdGimnasio)
        {
            return _gimnasio.Find(x => x.Id == IdGimnasio).First().Rutinas;
        }

        public Rutina Get(string idGimnasio, string idRutina)
        {

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("Rutinas", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(idRutina)) })))
                            });

            var e = _gimnasio.Find(filter).ToList();

            return e[0].Rutinas.Where(x => x.Id == idRutina).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idGimnasio"></param>
        /// <param name="rutina"></param>
        public async System.Threading.Tasks.Task InsertAsync(string idGimnasio, Rutina rutina)
        {
            if (!_rutinaValidador.Validar())
            {
                throw new Exception(_rutinaValidador.Error);
            }

            if (String.IsNullOrWhiteSpace(rutina.Id))
            {
                rutina.Id = ObjectId.GenerateNewId().ToString();
            }

            var builder = Builders<Gimnasio>.Filter;
            var filter = builder.Eq(x => x.Id, idGimnasio);
            var update = Builders<Gimnasio>.Update
                .AddToSet(x => x.Rutinas, rutina);
            await _gimnasio.UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async System.Threading.Tasks.Task UpdateAsync(string idGimnasio, Rutina rutina)
        {

            var update = Builders<Gimnasio>.Update.Set(x => x.Rutinas[-1], rutina);

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("Rutinas", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(rutina.Id)) })))
                            });

            var e = _gimnasio.Find(filter).ToList();

            await _gimnasio.UpdateOneAsync(filter, update);

        }

        /// <summary>
        /// Elimina un ejercicio de una rutina
        /// </summary>
        public void Remove(string idGimnasio, string idRutina)
        {

            var builder = Builders<Gimnasio>.Filter;
            var filterMain = builder.Eq("_Id", ObjectId.Parse(idGimnasio)); 

            var filterTotal = builder.Eq("Rutinas.Id", ObjectId.Parse(idRutina));

            _gimnasio.FindOneAndUpdate(
              filterMain,
              Builders<Gimnasio>.Update.PullFilter("Rutinas.$.Id", filterTotal));


        }

        /// <summary>
        /// Actualiza toda la información de la rutina
        /// </summary>
        public async System.Threading.Tasks.Task RefreshRutina(string idGimnasio, string IdRutina)
        {

            var r = Get(idGimnasio, IdRutina);


            r.Bloques.ForEach(b => {
                b.EjercicioActividad.ForEach(ea =>
                {
                    ea.Ejercicio = _ejercicioService.Get(idGimnasio, ea.Ejercicio.Id);
                });
            });

            await UpdateAsync(idGimnasio, r);
        }
        

    }
}
