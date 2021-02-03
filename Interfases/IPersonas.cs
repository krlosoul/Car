using System;
using System.Collections.Generic;
using System.Text;

namespace Interfases
{
    public interface IPersonas
    {
        int id { get; set; }

        string nombres { get; set; }
        string apellidos { get; set; }
    }
}
