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

namespace Usuario_Aplicacion.Handlers
{
    public class EliminarUsuarioHandler : IRequestHandler<EliminarUsuarioCommand, bool>
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly ILogger<EliminarUsuarioHandler> _logger;
        public EliminarUsuarioHandler(IUsuarioRepo usuarioRepo, ILogger<EliminarUsuarioHandler> logger)
        {
            _usuarioRepo = usuarioRepo;
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
            await _usuarioRepo.EliminarAsync(usuario);
            _logger.LogInformation("Usuario con Id: {Id} eliminado exitosamente.", usuario.Id);
            return true;
        }
    }
}
