using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Usuario_Dominio.Objetos_de_Valor
{
    public record NombreCompleto 
    {
        public string Valor { get; init; }
        public NombreCompleto(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(valor));
            if (!valor.Contains(' '))
                throw new ArgumentException("El nombre debe contener al menos un nombre y apellido", nameof(valor));
            if (Regex.IsMatch(valor, @"\d"))
                throw new ArgumentException("El nombre no puede contener números.", nameof(valor));


            Valor = valor.Trim(); 
        }
        public override string ToString() => Valor;
    }
}
