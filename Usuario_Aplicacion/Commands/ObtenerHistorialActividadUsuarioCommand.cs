using MediatR;
using System.Collections.Generic;
using Usuario_Aplicacion.DTOs;

namespace Usuario_Aplicacion.Commands
{
    public record ObtenerHistorialActividadUsuarioCommand(string UsuarioId) : IRequest<IEnumerable<HistorialActividadDto>>;
}
