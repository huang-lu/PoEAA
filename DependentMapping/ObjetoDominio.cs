using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependentMapping
{
    class ObjetoDominio
    {
        public const long ID_SUSTITUTA = -1;
        public long id = ID_SUSTITUTA;
        public bool esIdNueva()
        {
            return id == ID_SUSTITUTA;
        }
    }
}
