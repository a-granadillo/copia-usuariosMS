using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario_Aplicacion.DTOs;

namespace Usuario_Aplicacion.Query
{
    public record ObtenerUsuarioQuery(string Id) : IRequest<UsuarioDto>;
}