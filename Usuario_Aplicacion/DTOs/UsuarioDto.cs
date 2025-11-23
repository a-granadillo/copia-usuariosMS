using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario_Aplicacion.DTOs
{
    public class UsuarioDto
    {
        public string Id { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string NumTelefono { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
