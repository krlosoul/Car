using System;
using System.Collections.Generic;
using System.Text;

namespace Interfases
{
    public interface IUsuarios
    {
        int id { get; set; }
        int idPersona { get; set; }

        string usuario { get; set; }
        string clave { get; set; }
    }
}
