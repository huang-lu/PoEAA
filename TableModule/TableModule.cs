using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    class TableModule
    {
        protected DataTable tabla;
        protected TableModule(DataSet esquema, string nombreTabla)
        {
            tabla = esquema.Tables[nombreTabla];
        }
    }
}
