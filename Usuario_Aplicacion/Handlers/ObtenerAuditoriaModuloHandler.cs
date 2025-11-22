using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Usuario_Aplicacion.DTOs;
using Usuario_Aplicacion.Commands;
using Usuario_Dominio.Repositorios;
using Microsoft.Extensions.Logging;

namespace Usuario_Aplicacion.Handlers
{
    public class ObtenerAuditoriaModuloHandler : IRequestHandler<ObtenerAuditoriaModuloCommand, IEnumerable<AuditoriaDto>>
    {
        private readonly IAuditoriaRepo _auditoriaRepo;
        private readonly ILogger<ObtenerAuditoriaModuloHandler> _logger;

        public ObtenerAuditoriaModuloHandler(IAuditoriaRepo auditoriaRepo, ILogger<ObtenerAuditoriaModuloHandler> logger)
        {
            _auditoriaRepo = auditoriaRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<AuditoriaDto>> Handle(ObtenerAuditoriaModuloCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consultando reporte de auditoría para el módulo: {Modulo}", request.Modulo);

            // 1. Buscamos en la base de datos usando el puerto (Interfaz)
            var logs = await _auditoriaRepo.ObtenerPorModuloAsync(request.Modulo);

            // 2. Convertimos (Mapeamos) las Entidades a DTOs
            var dtos = logs.Select(a => new AuditoriaDto
            {
                Id = a.Id,
                UsuarioId = a.UsuarioId,
                Accion = a.Accion,
                Modulo = a.Modulo,
                Detalles = a.Detalles,
                Nivel = a.Nivel,
                Fecha = a.Fecha
            });

            return dtos;
        }
    }
}