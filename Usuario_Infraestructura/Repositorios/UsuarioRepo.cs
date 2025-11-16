using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Objetos_de_Valor;
using Usuario_Dominio.Repositorios;
using Usuario_Infraestructura.Configuracion;
using Usuario_Infraestructura.Mapping;

namespace Usuario_Infraestructura.Repositorios
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly IMongoCollection<Usuario> _usuarios;
        private readonly ILogger<UsuarioRepo> _logger;
        public UsuarioRepo(MongoConfig config, ILogger<UsuarioRepo> logger)
        {
            _logger = logger;
            if(string.IsNullOrEmpty(config.ConnectionString))
                throw new ArgumentNullException(nameof(config.ConnectionString), "La conexion de MongoDB no puede ser nula");
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _usuarios = database.GetCollection<Usuario>("Cluster0");
            MongoMappingConfig.ConfigurarMapeos();
            _logger.LogDebug("Mongo conectado a la base de datos: {DatabaseName}", config.DatabaseName);
        }
        public async Task<Usuario?> ObtenerPorIdAsync(string id)
        {
            return await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Usuario?> ObtenerPorCorreoAsync(Usuario_Dominio.Objetos_de_Valor.Correo correo)
        {
            return await _usuarios.Find(u => u.Correo == correo).FirstOrDefaultAsync();
        }
        public async Task AgregarAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
            _logger.LogDebug("Usuario agregado con ID: {UsuarioId}", usuario.Id);
        }
        public async Task ActualizarAsync(Usuario usuario)
        {
            var resultado = await _usuarios.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);
            if (resultado.MatchedCount == 0)
            {
                _logger.LogWarning("Intento de actualizar usuario no existente con ID: {UsuarioId}", usuario.Id);
                throw new InvalidOperationException($"Usuario con ID '{usuario.Id}' no encontrado para actualizar.");
            }
            _logger.LogDebug("Usuario actualizado con ID: {UsuarioId}", usuario.Id);
        }
        public async Task EliminarAsync(Usuario usuario)
        {
            var resultado = await _usuarios.DeleteOneAsync(u => u.Id == usuario.Id);
            if (resultado.DeletedCount == 0)
            {
                _logger.LogWarning("Intento de eliminar usuario no existente con ID: {UsuarioId}", usuario.Id);
                throw new InvalidOperationException($"Usuario con ID '{usuario.Id}' no encontrado para eliminar.");
            }
            _logger.LogDebug("Usuario eliminado con ID: {UsuarioId}", usuario.Id);
        }
    }
}
