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
    public class ObtenerHistorialActividadUsuarioHandler : IRequestHandler<ObtenerHistorialActividadUsuarioCommand, IEnumerable<HistorialActividadDto>>
    {
        private readonly IHistorialActividadRepo _historialRepo;
        private readonly ILogger<ObtenerHistorialActividadUsuarioHandler> _logger;

        public ObtenerHistorialActividadUsuarioHandler(IHistorialActividadRepo historialRepo, ILogger<ObtenerHistorialActividadUsuarioHandler> logger)
        {
            _historialRepo = historialRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<HistorialActividadDto>> Handle(ObtenerHistorialActividadUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consultando historial de actividades para el usuario {UsuarioId}", request.UsuarioId);
            var actividades = await _historialRepo.ObtenerPorUsuarioIdAsync(request.UsuarioId);
            var dtos = actividades.Select(a => new HistorialActividadDto
            {
                Id = a.Id,
                UsuarioId = a.UsuarioId,
                Accion = a.Accion,
                Fecha = a.Fecha
            });

            return dtos;
        }
    }
}