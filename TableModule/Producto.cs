using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    public enum TipoProducto { W, S, DB };

    class Producto : TableModule
    {
        public Producto(DataSet esquema) : base(esquema, "Producto") { }

        public DataRow this[int key]
        {
            get
            {
                string filter = String.Format("ProductoId = {0}", key);
                return tabla.Select(filter)[0];
            }
        }

        public TipoProducto getTipoProducto(int productoId)
        {
            string codigoTipo = (string)this[productoId]["Tipo"];
            return (TipoProducto)Enum.Parse(typeof(TipoProducto), codigoTipo);
        }
    }
}
