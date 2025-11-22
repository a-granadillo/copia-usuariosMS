using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Usuario_Aplicacion.Commands;
using Usuario_Aplicacion.Excepciones;
using Usuario_Dominio.Repositorios;
using Usuario_Dominio.Entidades;

namespace Usuario_Aplicacion.Handlers
{
    public class EliminarUsuarioHandler : IRequestHandler<EliminarUsuarioCommand, bool>
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly IHistorialActividadRepo _historialRepo;
        private readonly IAuditoriaRepo _auditoriaRepo;
        private readonly IMediator _mediator;

        private readonly ILogger<EliminarUsuarioHandler> _logger;

        public EliminarUsuarioHandler(
            IUsuarioRepo usuarioRepo,
            IHistorialActividadRepo historialRepo,
            IAuditoriaRepo auditoriaRepo,
            IMediator mediator,
            ILogger<EliminarUsuarioHandler> logger)
        {
            _usuarioRepo = usuarioRepo;
            _historialRepo = historialRepo;
            _auditoriaRepo = auditoriaRepo;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepo.ObtenerPorIdAsync(request.Id);
            if (usuario == null)
            {
                _logger.LogWarning("Intento de eliminar usuario no existente con Id: {Id}", request.Id);
                throw new UsuarioNoEncontradoExc($"El usuario con Id {request.Id} no fue encontrado.");
            }

            // 3. Eliminar Usuario
            await _usuarioRepo.EliminarAsync(usuario);
            // 4. Guardar en Historial
            // Aunque el usuario se borre, el registro histórico queda en la colección de historial
            var historial = new HistorialActividad(usuario.Id, "Cuenta eliminada permanentemente");
            await _historialRepo.AgregarAsync(historial);

            // 5. Registrar en Auditoría
            var auditoriaCmd = new RegistrarAuditoriaCommand(
                usuario.Id,
                "Eliminación de Usuario",
                "USUARIOS",
                "El usuario ha sido eliminado del sistema",
                "Warning"
            );
            await _mediator.Send(auditoriaCmd, cancellationToken);

            // ----------------

            _logger.LogInformation("Usuario con Id: {Id} eliminado exitosamente.", usuario.Id);
            return true;
        }
    }
}
