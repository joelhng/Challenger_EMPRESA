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
    public class UsuarioService
    {
        private IMongoCollection<Usuario> _usuario;
        private ISettingsMDB _settingsMDB;

        UsuarioValidador _usuarioValidador = new UsuarioValidador();

        public UsuarioService(ISettingsMDB settingsMDB)
        {
            _settingsMDB = settingsMDB;
            _usuario = MongoSetting.SettingsUtil.GetDb(settingsMDB).GetCollection<Usuario>(settingsMDB.UsuarioCollection);

        }

        public Usuario Get(string Id)
        {

            return _usuario.Find(x => x.Id == Id).FirstOrDefault();
        }

        public List<Usuario> GetxGimnasio(string Id)
        {

            var usuarios = _usuario.Find(x => x.IdGimnasio == Id);
            return usuarios.ToList();
        }

        public Usuario GetxCorreo(string correo)
        {
            return _usuario.Find(x => x.Correo == correo).FirstOrDefault();
        }

        public void Insert(Usuario usuario)
        {
            if (_usuarioValidador.Validacion(usuario))
            {
                throw new Exception(_usuarioValidador.Error.ToString());
            }

            if (Existe(usuario))
            {
                throw new Exception("El usuario que intenta agregar ya existe.");
            }

            //Ejecución de validaciones.
            _usuario.InsertOne(usuario);
        }


        public void Update(Usuario usuario)
        {

            if (_usuarioValidador.Validacion(usuario))
            {
                throw new Exception(_usuarioValidador.Error.ToString());
            }

            var filter = Builders<Usuario>.Filter.Eq("Id", usuario.Id);

            var update = Builders<Usuario>.Update
                .Set("Nombre", usuario.Nombre)
                .Set("Telefono", usuario.Telefono)
                .Set("FechaNacimiento", usuario.FechaNacimiento)
                .Set("Contrasena", usuario.Contrasena)
                .Set("Genero", usuario.Genero)
                .Set("Identificacion", usuario.Identificacion)
                .Set("Objetivo", usuario.Objetivo)
                .Set("Nivel", usuario.Nivel)
                .Set("Bloqueado", usuario.Bloqueado);


            _usuario.UpdateOne(filter, update);
        }

        public void UpdateCertificado(string id, CertificadoPreventivo certificado)
        {

            var filter = Builders<Usuario>.Filter.Eq("Id", id);

            if (String.IsNullOrWhiteSpace(certificado.Id))
            {
                certificado.Id = ObjectId.GenerateNewId().ToString();
            }

            var update = Builders<Usuario>.Update
                .Set("CertificadoPreventivo", certificado);

            _usuario.UpdateOne(filter, update);
        }

        public bool Existe(Usuario usuario)
        {

            return _usuario.Find(x => x.Correo == usuario.Correo).Any();
        }


        public List<Rutina> GetRutinaUsuarios(string idGimnasio, string IdRutina, bool soloAsingados)
        {

            var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idGimnasio))
                                , new BsonElement("Rutinas", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(IdRutina)) })))
                            });

            var e = _usuario.Find(filter).ToList();


            return null;
        }


        public async void SetRutinaUsuario(string idGimnasio, string idUsuario, string idRutina, bool check)
        {

            if (check)
            {
                var rutinaService = new RutinaService(_settingsMDB);


                var rutina = rutinaService.Get(idGimnasio, idRutina);

                var builder = Builders<Usuario>.Filter;
                var filter = builder.Eq(x => x.Id, idUsuario);
                var update = Builders<Usuario>.Update
                    .AddToSet(x => x.Rutinas, rutina);
                await _usuario.UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = true });

            }
            else
            {

                var u = Get(idUsuario);

                var r = u.Rutinas.Where(x => x.Id != idRutina).ToList();

                var update = Builders<Usuario>.Update.Set(x => x.Rutinas, r);

                var filter = new BsonDocument(new List<BsonElement> {
                            new BsonElement("_id", ObjectId.Parse(idUsuario))
                                , new BsonElement("Rutinas", new BsonDocument("$elemMatch", new BsonDocument(new List<BsonElement> { new BsonElement("_id",  ObjectId.Parse(idRutina)) })))
                            });

                var e = _usuario.Find(filter).ToList();

                await _usuario.UpdateOneAsync(filter, update);

            }

        }

    }
}
