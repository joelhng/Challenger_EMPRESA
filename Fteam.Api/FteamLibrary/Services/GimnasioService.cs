using FteamLibrary.Entities;
using FteamLibrary.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FteamLibrary.Services
{
    public class GimnasioService
    {

        private IMongoCollection<Gimnasio> _gimnasio;

        public GimnasioService(ISettingsMDB settingsMDB)
        {
            _gimnasio = MongoSetting.SettingsUtil.GetDb(settingsMDB).GetCollection<Gimnasio>(settingsMDB.GimnasioCollection);
        }

        /// <summary>
        /// Recupera todos los gimnasio 
        /// </summary>
        /// <returns></returns>
        public List<Gimnasio> Get()
        {
            return _gimnasio.Find(x => true).ToList();
        }

        public Gimnasio Get(string Id)
        {
            return _gimnasio.Find(x => x.Id == Id).First();
        }

        /// <summary>
        /// Recupera por el código representativo del gimnasio
        /// El código es un valor asignado manualmente al registrar el gimnasio.
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public string GetIdxCodigo(string Codigo)
        {

            return _gimnasio.Find(x => x.Codigo == Codigo).First().Id;
        }

        public string GetIdxCorreo(string correo)
        {

            return _gimnasio.Find(x => x.Correo == correo).First().Id;
        }

        public bool Existe(string correo)
        {
            return _gimnasio.Find(x => x.Correo == correo).Any();
        }

        public Gimnasio Insert(Gimnasio gimnasio)
        {


            if (Existe(gimnasio.Correo))
            {
                return gimnasio;
            }

            _gimnasio.InsertOne(gimnasio);

            return gimnasio;
        }



    }
}
