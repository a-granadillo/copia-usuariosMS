using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Usuario_Aplicacion.Commands
{
    public record EliminarUsuarioCommand(string Id) : IRequest<bool>;
}
