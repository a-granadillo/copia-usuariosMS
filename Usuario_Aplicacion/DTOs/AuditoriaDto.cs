using System;

namespace Usuario_Aplicacion.DTOs
{
    public class AuditoriaDto
    {
        public string Id { get; set; } = string.Empty;
        public string? UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string Detalles { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}