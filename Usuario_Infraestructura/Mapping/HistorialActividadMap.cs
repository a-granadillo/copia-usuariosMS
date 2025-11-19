using MongoDB.Bson.Serialization;
using Usuario_Dominio.Entidades; 

namespace Usuario_Infraestructura.Mapping
{
    public static class HistorialActividadMap
    {
        public static void ConfigurarMapeos()
        {
            
            if (BsonClassMap.IsClassMapRegistered(typeof(HistorialActividad))) return;

            BsonClassMap.RegisterClassMap<HistorialActividad>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdProperty(x => x.Id);
                map.MapProperty(x => x.UsuarioId).SetElementName("UsuarioId");
                map.MapProperty(x => x.Accion).SetElementName("Accion");
                map.MapProperty(x => x.Fecha).SetElementName("Fecha");
            });
        }
    }
}
