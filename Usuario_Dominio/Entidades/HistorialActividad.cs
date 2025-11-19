using System;

namespace Usuario_Dominio.Entidades
{
    public class HistorialActividad
    {
        public string Id { get; private set; }
        public string UsuarioId { get; private set; }
        public string Accion { get; private set; }
        public DateTime Fecha { get; private set; }

        public HistorialActividad(string usuarioId, string accion)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID del usuario es requerido.", nameof(usuarioId));

            if (string.IsNullOrWhiteSpace(accion))
                throw new ArgumentException("La descripción de la acción es requerida.", nameof(accion));

            Id = Guid.NewGuid().ToString();
            UsuarioId = usuarioId;
            Accion = accion;
            Fecha = DateTime.UtcNow;
        }

        private HistorialActividad() { }
    }
}