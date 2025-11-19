using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Repositorios;
using Usuario_Infraestructura.Configuracion;
using Microsoft.Extensions.Logging;

namespace Usuario_Infraestructura.Repositorios
{
    public class HistorialActividadRepo : IHistorialActividadRepo
    {
        private readonly IMongoCollection<HistorialActividad> _collection;
        private readonly ILogger<HistorialActividadRepo> _logger;

        public HistorialActividadRepo(MongoConfig config, ILogger<HistorialActividadRepo> logger)
        {
            _logger = logger;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _collection = database.GetCollection<HistorialActividad>("HistorialActividades");
        }

        public async Task AgregarAsync(HistorialActividad actividad)
        {
            await _collection.InsertOneAsync(actividad);
            _logger.LogInformation("Actividad guardada: {Accion} para Usuario {UsuarioId}", actividad.Accion, actividad.UsuarioId);
        }

        public async Task<IEnumerable<HistorialActividad>> ObtenerPorUsuarioIdAsync(string usuarioId)
        {
            return await _collection
                .Find(x => x.UsuarioId == usuarioId)
                .SortByDescending(x => x.Fecha) 
                .ToListAsync();
        }
    }
}
