using FteamLibrary.Entities;
using FteamLibrary.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamLibrary.Services
{
    public class HorariosService
    {
        private IMongoCollection<Gimnasio> _gimnasio;        

        public HorariosService(ISettingsMDB settingsMDB)
        {
            _gimnasio = MongoSetting.SettingsUtil.GetDb(settingsMDB).GetCollection<Gimnasio>(settingsMDB.GimnasioCollection);        }


        public List<HorariosDisponibles> Get(string IdGimnasio)
        {
            return _gimnasio.Find(x => x.Id == IdGimnasio).First().HorariosDisponibles;
        }

        public HorariosDisponibles Get(string idGimnasio, string IdHorario)
        {

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("HorariosDisponibles", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(IdHorario)) })))
                            });

            var e = _gimnasio.Find(filter).ToList();

            return e[0].HorariosDisponibles.Where(x => x.Id == IdHorario).First();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idGimnasio"></param>
        /// <param name="HorariosDisponibles"></param>
        public async System.Threading.Tasks.Task InsertAsync(string idGimnasio, HorariosDisponibles horariosDisponibles)
        {
        
            if (String.IsNullOrWhiteSpace(horariosDisponibles.Id))
            {
                horariosDisponibles.Id = ObjectId.GenerateNewId().ToString();
            }

            var builder = Builders<Gimnasio>.Filter;
            var filter = builder.Eq(x => x.Id, idGimnasio);
            var update = Builders<Gimnasio>.Update
                .AddToSet(x => x.HorariosDisponibles, horariosDisponibles);
            await _gimnasio.UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async System.Threading.Tasks.Task UpdateAsync(string idGimnasio, HorariosDisponibles horariosDisponibles)
        {

            var update = Builders<Gimnasio>.Update.Set(x => x.HorariosDisponibles[-1], horariosDisponibles);

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("HorariosDisponibles", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(horariosDisponibles.Id)) })))
                            });
           

            await _gimnasio.UpdateOneAsync(filter, update);

        }

        public void Remove(string idGimnasio, string idHorario)
        {
            var g = _gimnasio.Find(x => x.Id == idGimnasio).First();

            var horarios = g.HorariosDisponibles.Where(x => x.Id != idHorario).ToList();

            var update = Builders<Gimnasio>.Update.Set(x => x.HorariosDisponibles, horarios);

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))                                
                            });


            _gimnasio.UpdateOneAsync(filter, update);


            //var builder = Builders<Gimnasio>.Filter;
            //var filterMain = builder.Eq("_id", ObjectId.Parse(idGimnasio));

            //var filterTotal = builder.Eq("HorariosDisponibles.Id", ObjectId.Parse(idHorario));

            //_gimnasio.FindOneAndUpdate(
            //  filterMain,
            //  Builders<Gimnasio>.Update.PullFilter("HorariosDisponibles.$.Id", filterTotal));


        }


    }
}
