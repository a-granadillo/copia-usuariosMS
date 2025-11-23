using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario_Aplicacion.DTOs;
using Usuario_Aplicacion.Query;
using Usuario_Dominio.Repositorios;

namespace Usuario_Aplicacion.Handlers
{
    public class ObtenerUsuarioHandler : IRequestHandler<ObtenerUsuarioQuery, UsuarioDto>
    {
        private readonly IUsuarioRepo _repo;
        public ObtenerUsuarioHandler(IUsuarioRepo repo) { _repo = repo; }
        public async Task<UsuarioDto> Handle(ObtenerUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _repo.ObtenerPorIdAsync(request.Id);
            if (usuario == null)
                return null;
            return new UsuarioDto { Id = usuario.Id, NombreCompleto = usuario.NombreCompleto.Valor, Correo = usuario.Correo.DireccionCorreo, NumTelefono = usuario.NumTelefono.Numero, Rol = usuario.rol };
        }
    }
}