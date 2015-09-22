using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    class Contrato : TableModule
    {
        public Contrato(DataSet esquema) : base(esquema, "Contrato") { }

        public DataRow this[int key]
        {
            get
            {
                string filter = String.Format("ContratoId = {0}", key);
                return table.Select(filter)[0];
            }
        }

        public void calcularReconocimiento(int contratoID)
        {
            DataRow filaContrato = this[contratoID];
            decimal ingreso = (decimal)filaContrato["Ingreso"];
            ReconocimientoIngreso reconocimientoIngreso = new ReconocimientoIngreso(table.DataSet);
            Producto producto = new Producto(table.DataSet);
            int productoId = getProductoId(contratoID);
            if (producto.getTipoProducto(productoId) == TipoProducto.W)
            {
                reconocimientoIngreso.insertar(contratoID, ingreso, (DateTime)getFechaFirma(contratoID));
            }
            else if (producto.getTipoProducto(productoId) == TipoProducto.S)
            {
                decimal[] fraccion = fraccionar(ingreso, 3);
                reconocimientoIngreso.insertar(contratoID, fraccion[0],
                    (DateTime)getFechaFirma(contratoID));
                reconocimientoIngreso.insertar(contratoID, fraccion[1],
                    (DateTime)getFechaFirma(contratoID).AddDays(60));
                reconocimientoIngreso.insertar(contratoID, fraccion[2],
                    (DateTime)getFechaFirma(contratoID).AddDays(90));
            }
            else if (producto.getTipoProducto(productoId) == TipoProducto.DB)
            {
                decimal[] fraccion = fraccionar(ingreso, 3);
                reconocimientoIngreso.insertar(contratoID, fraccion[0],
                    (DateTime)getFechaFirma(contratoID));
                reconocimientoIngreso.insertar(contratoID, fraccion[1],
                    (DateTime)getFechaFirma(contratoID).AddDays(30));
                reconocimientoIngreso.insertar(contratoID, fraccion[2],
                    (DateTime)getFechaFirma(contratoID).AddDays(60));
            }
            else
            {
                throw new Exception("ProductoId no válido");
            }
        }

        private DateTime getFechaFirma(int contratoID)
        {
            return (DateTime)this[contratoID]["Fecha"];
        }

        private int getProductoId(int contratoID)
        {
            return (int)this[contratoID]["ProductoId"];
        }

        private Decimal[] fraccionar(decimal ingreso, int nPartes)
        {
            decimal valorMinimo = ingreso / nPartes;

            valorMinimo = Decimal.Round(valorMinimo, 2);
            decimal valorMaximo = valorMinimo + 0.01m;
            decimal[] resultado = new Decimal[nPartes];
            int resto = (int)ingreso % nPartes;
            for (int i = 0; i < resto; i++) resultado[i] = valorMaximo;
            for (int i = resto; i < nPartes; i++) resultado[i] = valorMinimo;
            return resultado;
        }
    }
}
