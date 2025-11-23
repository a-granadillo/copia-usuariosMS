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
        private readonly IMediator _mediator;

        // 1. USAMOS TU CONSTRUCTOR (Con las 4 dependencias necesarias para auditoría)
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
            // 2. USAMOS LA LÓGICA DE ELLA (Para crear los objetos y tomar el ID y ROL correctos)
            var nombre = new NombreCompleto(request.NombreCompleto);
            var correo = new Correo(request.Correo);
            var telefono = new NumTelefono(request.NumTelefono);

            // OJO: Usamos request.IdUsuarioKeycloak (de ella) en vez de Guid.NewGuid (tuyo)
            var usuario = new Usuario(id: request.IdUsuarioKeycloak, nombreCompleto: nombre, correo: correo, numTelefono: telefono, rol: request.rol);

            await _usuarioRepo.AgregarAsync(usuario);

            // 3. USAMOS TU LÓGICA (Para guardar historial y auditoría)
            var historial = new HistorialActividad(usuario.Id, "Cuenta de usuario creada");
            await _historialRepo.AgregarAsync(historial);

            var auditoriaCmd = new RegistrarAuditoriaCommand(
                usuario.Id,
                "Creación de Usuario",
                "USUARIOS",
                $"Se registró el usuario: {usuario.Correo} con Rol: {request.rol}",
                "Info"
            );
            await _mediator.Send(auditoriaCmd, cancellationToken);

            // 4. LOG Y RETORNO DE ELLA (Para incluir el Rol en la respuesta)
            _logger.LogInformation("Usuario creado: {UsuarioId}, Rol={Rol}, {EmailEnmascarado} ", usuario.Id, request.rol, Enmascarado.EmailEnmascarado(usuario.Correo.ToString()));
            
            return new UsuarioDto 
            { 
                Id = usuario.Id, 
                NombreCompleto = usuario.NombreCompleto.Valor, 
                Correo = usuario.Correo.DireccionCorreo, 
                NumTelefono = usuario.NumTelefono.Numero, 
                Rol = request.rol 
            };
        }
    }
}
