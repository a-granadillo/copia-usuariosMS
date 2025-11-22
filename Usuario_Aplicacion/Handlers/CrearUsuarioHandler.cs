using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Objetos_de_Valor;
using Usuario_Dominio.Repositorios;
using Usuario_Aplicacion.DTOs;
using Usuario_Aplicacion.Commands;
using Usuario_Aplicacion.Comun;

namespace Usuario_Aplicacion.Handlers
{
    public class CrearUsuarioHandler : IRequestHandler<CrearUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly ILogger<CrearUsuarioHandler> _logger;
        private readonly IHistorialActividadRepo _historialRepo;
        private readonly IMediator _mediator; // Necesario para llamar al comando de Auditoría

        public CrearUsuarioHandler(
            IUsuarioRepo usuarioRepo,
            ILogger<CrearUsuarioHandler> logger,
            IHistorialActividadRepo historialRepo,
            IMediator mediator)
        {
            _usuarioRepo = usuarioRepo;
            _logger = logger;
            _historialRepo = historialRepo;
            _mediator = mediator;
        }

        public async Task<UsuarioDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            // --- LÓGICA ORIGINAL (Intacta) ---
            // Simulamos la creación en Keycloak y obtenemos un ID
            var keycloakId = Guid.NewGuid().ToString();
            var nombre = new NombreCompleto(request.NombreCompleto);
            var correo = new Correo(request.Correo);
            var telefono = new NumTelefono(request.NumTelefono);
            var usuario = new Usuario(id: keycloakId, nombreCompleto: nombre, correo: correo, numTelefono: telefono);

            await _usuarioRepo.AgregarAsync(usuario);

            // 1. Guardar en tu Historial
            var historial = new HistorialActividad(usuario.Id, "Cuenta de usuario creada");
            await _historialRepo.AgregarAsync(historial);
            // 2. Mandar a Auditoría (Usando tu comando)
            var auditoriaCmd = new RegistrarAuditoriaCommand(
                usuario.Id,
                "Creación de Usuario",
                "USUARIOS",
                $"Se registró el usuario: {usuario.Correo}",
                "Info"
            );
            await _mediator.Send(auditoriaCmd, cancellationToken);

            // ----------------------------------

            _logger.LogInformation("Usuario creado con ID y correo: {UsuarioId}, {EmailEnmascarado} ", usuario.Id, Enmascarado.EmailEnmascarado(usuario.Correo.ToString()));

            return new UsuarioDto
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto.Valor,
                Correo = usuario.Correo.DireccionCorreo,
                NumTelefono = usuario.NumTelefono.Numero
            };
        }
    }
}
