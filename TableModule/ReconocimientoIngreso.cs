using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    class ReconocimientoIngreso : TableModule
    {
        public ReconocimientoIngreso(DataSet esquema) : base(esquema, "ReconocimientoIngreso") { }

        public void insertar(int contratoId, decimal cantidad, DateTime fechaFirma)
        {
            DataRow nuevaFila = tabla.NewRow();
            nuevaFila["ContratoId"] = contratoId;
            nuevaFila["Cantidad"] = cantidad;
            nuevaFila["FechaReconocimiento"] = fechaFirma;
            tabla.Rows.Add(nuevaFila);
        }

        public decimal ingresoReconocido(int contratoId, DateTime fecha)
        {
            string filtro = String.Format("ContratoId = {0} AND FechaReconocimiento <= #{1:d}#",
                contratoId, fecha);
            DataRow[] filas = tabla.Select(filtro);
            decimal resultado = 0m;
            foreach (DataRow fila in filas)
            {
                resultado += (decimal)fila["Cantidad"];
            }
            return resultado;
        }
    }
}
