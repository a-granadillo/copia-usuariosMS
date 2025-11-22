using System.Collections.Generic;
using System.Threading.Tasks;
using Usuario_Dominio.Entidades;

namespace Usuario_Dominio.Repositorios
{
    public interface IAuditoriaRepo
    {
        // Guardar un evento de auditoría
        Task AgregarAsync(Auditoria auditoria);

        // Consultar auditoría por módulo
        Task<IEnumerable<Auditoria>> ObtenerPorModuloAsync(string modulo);

        // Consultar todo
        Task<IEnumerable<Auditoria>> ObtenerTodoAsync();
    }
}
