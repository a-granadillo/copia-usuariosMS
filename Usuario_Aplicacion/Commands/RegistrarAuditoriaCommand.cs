using MediatR;

namespace Usuario_Aplicacion.Commands
{
    public record RegistrarAuditoriaCommand(
        string? UsuarioId,
        string Accion,
        string Modulo,
        string Detalles,
        string Nivel
    ) : IRequest<string>; // Devuelve el ID del log creado
}
