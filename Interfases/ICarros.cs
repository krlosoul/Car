using System;
using System.Collections.Generic;
using System.Text;

namespace Interfases
{
    public interface ICarros
    {
        int id { get; set; }
        string referenciaProducto { get; set; }
        int idUsuario { get; set; }

        int cantidad { get; set; }
        Decimal total { get; set; }
        int estado { get; set; }

        
    }
}
