using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Usuario_Dominio.Objetos_de_Valor
{
    public record NumTelefono 
    {
        public string Numero { get; init; }
        public NumTelefono(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("El número de teléfono no puede estar vacío.", nameof(numero));
            if (!System.Text.RegularExpressions.Regex.IsMatch(numero, @"^\+?[1-9]\d{1,14}$"))
                throw new ArgumentException("El número de teléfono no es válido.", nameof(numero));

            Numero = numero;
        }
        public override string ToString() => Numero;
    }
}
