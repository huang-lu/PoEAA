using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionScriptSample
{
    class Gateway
    {
        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }

        public SqlDataReader buscarReconocimientoPor(long contratoID, DateTime fecha)
        {
            SqlConnection conn = conectarBD();
            conn.Open();
            string transaccion = "SELECT Cantidad " +
                "FROM dbo.ReconocimientoIngreso "+
                "WHERE contratoId = @contrato AND FechaReconocimiento <= @fecha";
            SqlCommand busqueda = new SqlCommand(transaccion, conn);
            SqlParameter paramContrato = new SqlParameter("@contrato", SqlDbType.Int, 200);
            SqlParameter paramFecha = new SqlParameter("@fecha", SqlDbType.Date);
            paramContrato.Value = contratoID;
            paramFecha.Value = fecha.ToShortDateString();
            busqueda.Parameters.Add(paramContrato);
            busqueda.Parameters.Add(paramFecha);
            busqueda.Prepare();
            SqlDataReader resultado = busqueda.ExecuteReader();
            return resultado;
        }

        public SqlDataReader buscarContrato(long contratoId)
        {
            SqlConnection conn = conectarBD();
            conn.Open();
            string transaccion = "SELECT * " +
                "FROM Contrato C, Producto P " +
                "WHERE ContratoId = @contrato AND C.ProductoId = P.ProductoId";
            SqlCommand busqueda = new SqlCommand(transaccion, conn);
            SqlParameter parametroContrato = new SqlParameter("@contrato", SqlDbType.Int, 200);
            parametroContrato.Value = (int)contratoId;
            busqueda.Parameters.Add(parametroContrato);
            busqueda.Prepare();
            SqlDataReader resultado = busqueda.ExecuteReader();
            return resultado;
        }

        public void anadirReconocimiento(long contratoId, decimal ingreso, DateTime fecha)
        {
            SqlConnection conn = conectarBD();
            conn.Open();
            string transaccion = "INSERT INTO ReconocimientoIngreso VALUES (@contrato, @cantidad, @fecha)";
            SqlCommand busqueda = new SqlCommand(transaccion, conn);
            SqlParameter paramContrato = new SqlParameter("@contrato", SqlDbType.Int, 200);
            SqlParameter paramIngreso = new SqlParameter("@cantidad", SqlDbType.Decimal, 18);
            SqlParameter paramFecha = new SqlParameter("@fecha", SqlDbType.Date);
            paramContrato.Value = contratoId;
            paramIngreso.Value = ingreso;
            paramIngreso.Precision = 18;
            paramIngreso.Scale = 8;
            paramFecha.Value = fecha.ToShortDateString();
            busqueda.Parameters.Add(paramContrato);
            busqueda.Parameters.Add(paramIngreso);
            busqueda.Parameters.Add(paramFecha);
            busqueda.Prepare();
            busqueda.ExecuteNonQuery();
            conn.Close();
        }
    }
}
