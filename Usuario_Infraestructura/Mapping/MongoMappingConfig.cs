using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Usuario_Infraestructura.Mapping
{
    public static class MongoMappingConfig
    {
        private static bool _isConfigured = false;

        public static void ConfigurarMapeos()
        {
            if (_isConfigured) return;

            var exceptions = new List<Exception>();

            var mappingTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.IsSealed && t.IsAbstract) 
                .Where(t => t != typeof(MongoMappingConfig))        
                .Where(t => t.GetMethod("ConfigurarMapeos", BindingFlags.Public | BindingFlags.Static) != null);

            foreach (var type in mappingTypes)
            {
                var method = type.GetMethod("ConfigurarMapeos", BindingFlags.Public | BindingFlags.Static);
                try
                {
                    method?.Invoke(null, null);
                }
                catch (TargetInvocationException tie)
                {
                    
                    exceptions.Add(new InvalidOperationException(
                        $"Error registrando el mapeo '{type.Name}': {tie.InnerException?.Message}", tie.InnerException));
                }
                catch (Exception ex)
                {
                    exceptions.Add(new InvalidOperationException(
                        $"Error inesperado registrando el mapeo '{type.Name}': {ex.Message}", ex));
                }
            }

            _isConfigured = true;


            if (exceptions.Any())
            {
                throw new AggregateException("Se produjeron errores al registrar los mapas de MongoDB.", exceptions);
            }
        }
    }
}