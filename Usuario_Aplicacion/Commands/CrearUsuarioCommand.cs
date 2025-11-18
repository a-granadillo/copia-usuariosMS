using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Usuario_Aplicacion.DTOs;

namespace Usuario_Aplicacion.Commands
{
    public record CrearUsuarioCommand(string NombreCompleto, string Correo, string NumTelefono) : IRequest<UsuarioDto>;
}
