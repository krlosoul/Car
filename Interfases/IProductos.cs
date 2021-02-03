using System;
using System.Collections.Generic;
using System.Text;

namespace Interfases
{
    public interface IProductos
    {
        string referencia { get; set; }

        string descripcion { get; set; }
        Decimal valor { get; set; }
    }
}
