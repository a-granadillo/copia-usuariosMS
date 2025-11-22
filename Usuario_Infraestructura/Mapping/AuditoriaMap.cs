using MongoDB.Bson.Serialization;
using Usuario_Dominio.Entidades;

namespace Usuario_Infraestructura.Mapping
{
    public static class AuditoriaMap
    {
        public static void ConfigurarMapeos()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(Auditoria))) return;

            BsonClassMap.RegisterClassMap<Auditoria>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdProperty(x => x.Id);
                map.MapProperty(x => x.UsuarioId).SetElementName("UsuarioId");
                map.MapProperty(x => x.Accion).SetElementName("Accion");
                map.MapProperty(x => x.Modulo).SetElementName("Modulo");
                map.MapProperty(x => x.Nivel).SetElementName("Nivel");
                map.MapProperty(x => x.Detalles).SetElementName("Detalles");
                map.MapProperty(x => x.Fecha).SetElementName("Fecha");
            });
        }
    }
}
