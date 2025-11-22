using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Usuario_Aplicacion.DTOs;
using Usuario_Aplicacion.Commands;
using Usuario_Aplicacion.Excepciones;
using Usuario_Aplicacion.Comun;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Objetos_de_Valor;
using Usuario_Dominio.Repositorios;

namespace Usuario_Aplicacion.Handlers
{
    public class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly IHistorialActividadRepo _historialRepo;
        private readonly IAuditoriaRepo _auditoriaRepo;
        private readonly IMediator _mediator;

        private readonly ILogger<ActualizarUsuarioHandler> _logger;

        public ActualizarUsuarioHandler(
            IUsuarioRepo usuarioRepo,
            IHistorialActividadRepo historialRepo,
            IAuditoriaRepo auditoriaRepo,
            IMediator mediator,
            ILogger<ActualizarUsuarioHandler> logger)
        {
            _usuarioRepo = usuarioRepo;
            _historialRepo = historialRepo;
            _auditoriaRepo = auditoriaRepo;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<UsuarioDto> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepo.ObtenerPorIdAsync(request.Id);
            if (usuario == null)
            {
                _logger.LogWarning("Intento de actualizar usuario no existente con ID: {UsuarioId}", request.Id);
                throw new UsuarioNoEncontradoExc($"Usuario '{request.Id}' no encontrado");
            }

            var nuevoNombre = request.NombreCompleto != null ? new NombreCompleto(request.NombreCompleto) : usuario.NombreCompleto;
            var nuevoCorreo = request.Correo != null ? new Correo(request.Correo) : usuario.Correo;
            var nuevoTelefono = request.NumTelefono != null ? new NumTelefono(request.NumTelefono) : usuario.NumTelefono;

            usuario.ActualizarPerfil(nuevoNombre, nuevoCorreo, nuevoTelefono);

            // 3. Actualizar Usuario
            await _usuarioRepo.ActualizarAsync(usuario);

            // 4. Guardar en Historial
            var historial = new HistorialActividad(usuario.Id, "Perfil actualizado");
            await _historialRepo.AgregarAsync(historial);

            // 5. Registrar en Auditoría (vía comando)
            var auditoriaCmd = new RegistrarAuditoriaCommand(
                usuario.Id,
                "Actualización de Perfil",
                "USUARIOS",
                $"Se actualizaron datos del usuario: {usuario.Correo}",
                "Info"
            );
            await _mediator.Send(auditoriaCmd, cancellationToken);

            // ----------------

            _logger.LogInformation("Usuario actualizado con ID: {UsuarioId}", usuario.Id);

            return new UsuarioDto
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto.ToString(),
                Correo = usuario.Correo.ToString(),
                NumTelefono = usuario.NumTelefono.ToString()
            };
        }
    }
}
