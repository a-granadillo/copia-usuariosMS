using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario_Aplicacion.Comun
{
    public static class Enmascarado
    {
        public static string EmailEnmascarado(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) return "***";
            var parts = email.Split('@');
            var localPart = parts[0];
            var domainPart = parts[1];
            var visibleChars = Math.Min(2, localPart.Length);
            var maskedLocal = localPart.Substring(0, visibleChars) + new string('*', localPart.Length - visibleChars);
            return $"{maskedLocal}@{domainPart}";
        }
    }
}
