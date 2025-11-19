using System;

namespace Usuario_Aplicacion.DTOs
{
    public class HistorialActividadDto
    {
        public string Id { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
