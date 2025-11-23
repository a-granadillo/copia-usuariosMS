using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Usuario_Dominio.Objetos_de_Valor;

namespace Usuario_Dominio.Entidades
{
    public class Usuario
    {
        public string Id { get; set; }
        public NombreCompleto NombreCompleto { get; set; }
        public Correo Correo { get; set; }
        public NumTelefono NumTelefono { get; set; }
        public string rol { get; set; }

        private Usuario() { } 

        public Usuario(string id, NombreCompleto nombreCompleto, Correo correo, NumTelefono numTelefono, string rol)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            NombreCompleto = nombreCompleto ?? throw new ArgumentNullException(nameof(nombreCompleto));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            NumTelefono = numTelefono ?? throw new ArgumentNullException(nameof(numTelefono));
            this.rol = rol ?? throw new ArgumentNullException(nameof(rol));
        }
        public void ActualizarPerfil(NombreCompleto nombre, Correo correo, NumTelefono numTelefono)
        {
            NombreCompleto = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            NumTelefono = numTelefono ?? throw new ArgumentNullException(nameof(numTelefono));
        }

    }
}
