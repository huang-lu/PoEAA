using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    interface Consulta
    {
        string Sql();

        Hashtable Parametros();
    }
}
