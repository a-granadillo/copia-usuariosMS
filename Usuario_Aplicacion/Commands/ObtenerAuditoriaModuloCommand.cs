using MediatR;
using System.Collections.Generic;
using Usuario_Aplicacion.DTOs;

namespace Usuario_Aplicacion.Commands
{
    /// <summary>
    /// Comando para consultar los registros de auditoría filtrados por módulo.
    /// </summary>
    /// <param name="Modulo">El nombre del módulo a consultar (ej. "SEGURIDAD", "PAGOS").</param>
    public record ObtenerAuditoriaModuloCommand(string Modulo) : IRequest<IEnumerable<AuditoriaDto>>;
}
