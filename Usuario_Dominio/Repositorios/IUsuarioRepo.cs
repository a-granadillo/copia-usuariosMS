using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario_Dominio.Entidades;
using Usuario_Dominio.Objetos_de_Valor;

namespace Usuario_Dominio.Repositorios
{
    public interface IUsuarioRepo
    {
        Task<Usuario?> ObtenerPorIdAsync(string id);
        Task<Usuario?> ObtenerPorCorreoAsync(Objetos_de_Valor.Correo correo);
        Task AgregarAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(Usuario usuario);
    }
}
