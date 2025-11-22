using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Repositorios;
using Usuario_Infraestructura.Configuracion;
using Microsoft.Extensions.Logging; 

namespace Usuario_Infraestructura.Repositorios
{
    public class AuditoriaRepo : IAuditoriaRepo
    {
        private readonly IMongoCollection<Auditoria> _collection;
        private readonly ILogger<AuditoriaRepo> _logger;

        public AuditoriaRepo(MongoConfig config, ILogger<AuditoriaRepo> logger)
        {
            _logger = logger;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _collection = database.GetCollection<Auditoria>("Auditoria");
        }

        public async Task AgregarAsync(Auditoria auditoria)
        {
            _logger.LogInformation("Registrando auditoría: [{Nivel}] {Accion} en módulo {Modulo}",
                auditoria.Nivel, auditoria.Accion, auditoria.Modulo);

            await _collection.InsertOneAsync(auditoria);

            _logger.LogDebug("Auditoría guardada correctamente con ID: {Id}", auditoria.Id);
        }

        public async Task<IEnumerable<Auditoria>> ObtenerPorModuloAsync(string modulo)
        {
            _logger.LogInformation("Consultando auditoría para el módulo: {Modulo}", modulo);

            return await _collection
                .Find(x => x.Modulo == modulo)
                .SortByDescending(x => x.Fecha)
                .ToListAsync();
        }

        public async Task<IEnumerable<Auditoria>> ObtenerTodoAsync()
        {
            _logger.LogInformation("Consultando reporte completo de auditoría");

            return await _collection
                .Find(_ => true)
                .SortByDescending(x => x.Fecha)
                .ToListAsync();
        }
    }
}
