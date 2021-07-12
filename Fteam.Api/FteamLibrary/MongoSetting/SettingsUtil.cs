using FteamLibrary.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FteamLibrary.MongoSetting
{
    internal class SettingsUtil
    {
        public static IMongoDatabase GetDb(ISettingsMDB settingsMDB)
        {
            //mongodb + srv://joelhng:<password>@gymadmin.yznpo.mongodb.net/test
            //mongodb+srv://joelhng:<Tamara2208->@gymadmin.yznpo.mongodb.net/test

            //mongodb+srv://joelhng:Tamara2208@gymadmin.yznpo.mongodb.net/test
            //var connectionString = $"mongodb+srv://{settingsMDB.Usuario}:{settingsMDB.Contrasena}@{settingsMDB.Server}";
            var connectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";

            var cliente = new MongoClient(connectionString);
            var database = cliente.GetDatabase(settingsMDB.Database);

            return database;
        }
    }
}
