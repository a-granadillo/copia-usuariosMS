using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Usuario_Dominio.Entidades;

namespace Usuario_Infraestructura.Mapping
{
    public static class UsuarioMap
    {
        public static void ConfigurarMapeos()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Usuario)))
            {
                BsonClassMap.RegisterClassMap<Usuario>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapIdProperty(c => c.Id).SetElementName("Id");
                    map.MapProperty(c => c.NombreCompleto).SetElementName("Nombre");
                    map.MapProperty(c => c.Correo).SetElementName("Correo");
                    map.MapProperty(c => c.NumTelefono).SetElementName("NumTelefono");
                });
            }
        }
    }
}
