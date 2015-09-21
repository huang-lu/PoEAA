using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace TransactionScriptSample
{
    class RecognitionService
    {
        Gateway baseDatos;

        public RecognitionService()
        {
            baseDatos = new Gateway();
        }
        public decimal ingresosReconocidos(long contratoId, DateTime fecha)
        {
            decimal resultado = 0;
            SqlDataReader resultadoBusqueda = baseDatos.buscarReconocimientoPor(contratoId, fecha);
            while (resultadoBusqueda.Read())
            {
                resultado += resultadoBusqueda.GetDecimal(0);
            }
            return resultado;
        }

        public void calcularReconocimientoIngresos(long contratoId)
        {
            SqlDataReader contrato = baseDatos.buscarContrato(contratoId);
            contrato.Read();
            decimal ingresoTotal = Convert.ToDecimal(contrato["Ingreso"]);
            string tipo = contrato.GetString(6);
            DateTime fecha = contrato.GetDateTime(3);
            if (tipo.Equals("S"))
            {
                decimal[] fraccion = fraciona(ingresoTotal, 3);
                baseDatos.anadirReconocimiento(contratoId, fraccion[0], fecha);
                baseDatos.anadirReconocimiento(contratoId, fraccion[1], fecha.AddDays(60));
                baseDatos.anadirReconocimiento(contratoId, fraccion[2], fecha.AddDays(90));
            }
            else if (tipo.Equals("W"))
            {
                baseDatos.anadirReconocimiento(contratoId, ingresoTotal, fecha);
            }
            else
            {
                decimal[] fraccion = fraciona(ingresoTotal, 3);
                baseDatos.anadirReconocimiento(contratoId, fraccion[0], fecha);
                baseDatos.anadirReconocimiento(contratoId, fraccion[1], fecha.AddDays(30));
                baseDatos.anadirReconocimiento(contratoId, fraccion[2], fecha.AddDays(60));
            }
        }

        private decimal[] fraciona(decimal ingresoTotal, int nPartes)
        {
            decimal[] resultado = new decimal[3];
            decimal valorMinimo = ingresoTotal / nPartes;
            decimal valorMaximo = valorMinimo + 1;
            int resto = (int)ingresoTotal % nPartes;
            for (int i = 0; i < resto; i++)
            {
                resultado[i] = valorMaximo;
            }
            for (int i = resto; i < resultado.Length; i++)
            {
                resultado[i] = valorMinimo;
            }
            return resultado;
        }
    }
}
