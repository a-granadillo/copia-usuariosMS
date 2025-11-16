using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario_Aplicacion.Excepciones
{
    public class UsuarioNoEncontradoExc : Exception
    {
        public UsuarioNoEncontradoExc(string mensaje) : base(mensaje){}
    }
}
