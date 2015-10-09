using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    abstract class Repositorio
    {
        abstract public IList Coincidencias(Criterio criterio);
    }
}
