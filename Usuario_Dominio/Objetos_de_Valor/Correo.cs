using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Usuario_Dominio.Objetos_de_Valor
{
    public record Correo
    {
        private static readonly Regex PatronCorreo =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string DireccionCorreo { get; init; }
        public Correo(string direccionCorreo)
        {
            direccionCorreo = direccionCorreo.Trim();
            if (string.IsNullOrWhiteSpace(direccionCorreo))
                throw new ArgumentException("La dirección de correo no puede estar vacía.", nameof(direccionCorreo));
            if (!PatronCorreo.IsMatch(direccionCorreo))
                throw new ArgumentException("La dirección de correo no es válida.", nameof(direccionCorreo));

            DireccionCorreo = direccionCorreo.ToLowerInvariant();
        }
        public override string ToString() => DireccionCorreo;
    }
}
