using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Usuario_Dominio.Entidades;

namespace Usuario_Dominio.Repositorios
{
    public interface IHistorialActividadRepo
    {
        // Contrato para guardar
        Task AgregarAsync(HistorialActividad actividad);

        // Contrato para leer
        Task<IEnumerable<HistorialActividad>> ObtenerPorUsuarioIdAsync(string usuarioId);
    }
}
