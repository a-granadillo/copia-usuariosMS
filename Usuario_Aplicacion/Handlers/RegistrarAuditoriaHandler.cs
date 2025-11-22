using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Usuario_Aplicacion.Commands;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Repositorios;
using Microsoft.Extensions.Logging;

namespace Usuario_Aplicacion.Handlers
{
    public class RegistrarAuditoriaHandler : IRequestHandler<RegistrarAuditoriaCommand, string>
    {
        private readonly IAuditoriaRepo _auditoriaRepo;
        private readonly ILogger<RegistrarAuditoriaHandler> _logger;

        public RegistrarAuditoriaHandler(IAuditoriaRepo auditoriaRepo, ILogger<RegistrarAuditoriaHandler> logger)
        {
            _auditoriaRepo = auditoriaRepo;
            _logger = logger;
        }

        public async Task<string> Handle(RegistrarAuditoriaCommand request, CancellationToken cancellationToken)
        {
            var auditoria = new Auditoria(
                request.UsuarioId,
                request.Accion,
                request.Modulo,
                request.Detalles,
                request.Nivel
            );

            await _auditoriaRepo.AgregarAsync(auditoria);

            return auditoria.Id;
        }
    }
}
