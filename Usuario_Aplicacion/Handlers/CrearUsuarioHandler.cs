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
        public CrearUsuarioHandler(IUsuarioRepo usuarioRepo, ILogger<CrearUsuarioHandler> logger)
        {
            _usuarioRepo = usuarioRepo;
            _logger = logger;
        }
        public async Task<UsuarioDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            // Simulamos la creación en Keycloak y obtenemos un ID
            var nombre = new NombreCompleto(request.NombreCompleto);
            var correo = new Correo(request.Correo);
            var telefono = new NumTelefono(request.NumTelefono);

            var usuario = new Usuario(id: request.IdUsuarioKeycloak, nombreCompleto:nombre, correo:correo, numTelefono:telefono, rol: request.rol);

            await _usuarioRepo.AgregarAsync(usuario);
            _logger.LogInformation("Usuario creado: {UsuarioId}, Rol={Rol}, {EmailEnmascarado} ", usuario.Id, request.rol, Enmascarado.EmailEnmascarado(usuario.Correo.ToString()));
            return new UsuarioDto{Id = usuario.Id, NombreCompleto = usuario.NombreCompleto.Valor, Correo = usuario.Correo.DireccionCorreo, NumTelefono = usuario.NumTelefono.Numero, Rol = request.rol };
        }
    }
}
