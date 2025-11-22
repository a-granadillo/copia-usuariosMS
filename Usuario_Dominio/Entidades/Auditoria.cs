using System;

namespace Usuario_Dominio.Entidades
{
    public class Auditoria
    {
        public string Id { get; private set; }
        public string? UsuarioId { get; private set; } 
        public string Accion { get; private set; }
        public string Modulo { get; private set; } // Ej: "Usuarios", "Pagos"
        public string Detalles { get; private set; } 
        public string Nivel { get; private set; } // "Info", "Warning", "Critical"
        public DateTime Fecha { get; private set; }

        public Auditoria(string? usuarioId, string accion, string modulo, string detalles, string nivel)
        {
            if (string.IsNullOrWhiteSpace(accion))
                throw new ArgumentException("La acción es requerida.", nameof(accion));
            if (string.IsNullOrWhiteSpace(modulo))
                throw new ArgumentException("El módulo es requerido.", nameof(modulo));

            Id = Guid.NewGuid().ToString();
            UsuarioId = usuarioId; 
            Accion = accion;
            Modulo = modulo;
            Detalles = detalles ?? string.Empty;
            Nivel = nivel ?? "Info";
            Fecha = DateTime.UtcNow;
        }

        private Auditoria() { }
    }
}
