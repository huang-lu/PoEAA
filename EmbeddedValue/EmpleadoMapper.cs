using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class EmpleadoMapper
    {
        private static string cadenaBuscar = "SELECT * FROM Empleados WHERE EmpleadoId = @id";
        private static string cadenaInsertar = "INSERT INTO Empleados VALUES (@id, @nombre, @inicial, @final, @salario, @moneda)";
        private static string cadenaActualizar = @"UPDATE Empleados SET Nombre = @nombre,
                                                    Fecha_Inicial = @inicial,
                                                    Fecha_Final = @final,
                                                    Salario = @salario,
                                                    Moneda = @moneda
                                                    WHERE EmpleadoId = @id";
        private static string cadenaEliminar = "DELETE FROM Empleados WHERE EmpleadoId = @id";

        public static EmpleadoMapper instancia = new EmpleadoMapper();

        private IDictionary<long, Empleado> identityMap = new Dictionary<long, Empleado>();

        public void Refrescar()
        {
            identityMap.Clear();
        }

        public Empleado Buscar(long id)
        {
            Empleado resultado;
            if (identityMap.ContainsKey(id))
            {
                resultado = identityMap[id];
                if (resultado != null) { }
                {
                    return resultado;
                }
            }

            try
            {
                SqlCommand consulta = new SqlCommand(EmpleadoMapper.cadenaBuscar, BD());
                consulta.Parameters.AddWithValue("@id", id);
                consulta.Connection.Open();
                SqlDataReader fila = consulta.ExecuteReader();
                if (fila.HasRows)
                {
                    fila.Read();
                    string nombre = (string)fila["Nombre"];
                    DateTime inicial = (DateTime)fila["Fecha_Inicial"];
                    DateTime final = (DateTime)fila["Fecha_Final"];
                    double salario = (double)fila["Salario"];
                    string moneda = (string)fila["Moneda"];
                    resultado = new Empleado(id, nombre,
                        new RangoFecha(inicial, final),
                        new Dinero(salario, moneda));
                    identityMap.Add(id, resultado);
                    return resultado;
                }
                return null;
            }
            catch (SqlException e)
            {
                throw new ApplicationException(String.Format("No se ha podido encontrar a la persona {0}\n{1}", id, e));
            }
        }

        public void Insertar(Empleado empleado)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(EmpleadoMapper.cadenaInsertar, BD());
                consulta.Parameters.AddWithValue("@id", empleado.Id);
                consulta.Parameters.AddWithValue("@nombre", empleado.Nombre);
                consulta.Parameters.AddWithValue("@inicial", empleado.Periodo.Inicial);
                consulta.Parameters.AddWithValue("@final", empleado.Periodo.Final);
                consulta.Parameters.AddWithValue("@salario", empleado.Salario.Importe);
                consulta.Parameters.AddWithValue("@moneda", empleado.Salario.Moneda);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                identityMap.Add(empleado.Id, empleado);
            }
            catch (SqlException e)
            {
                throw new ApplicationException(String.Format("Imposible insertar emplado.\n{0}", e));
            }
        }

        public void Actualizar(Empleado empleado)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(EmpleadoMapper.cadenaActualizar, BD());
                consulta.Parameters.AddWithValue("@id", empleado.Id);
                consulta.Parameters.AddWithValue("@nombre", empleado.Nombre);
                consulta.Parameters.AddWithValue("@inicial", empleado.Periodo.Inicial);
                consulta.Parameters.AddWithValue("@final", empleado.Periodo.Final);
                consulta.Parameters.AddWithValue("@salario", empleado.Salario.Importe);
                consulta.Parameters.AddWithValue("@moneda", empleado.Salario.Moneda);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                if (identityMap.ContainsKey(empleado.Id))
                {
                    identityMap.Remove(empleado.Id);
                }
                identityMap.Add(empleado.Id, empleado);
            }
            catch (SqlException e)
            {
                throw new ApplicationException(String.Format("Imposible actualizar emplado.\n{0}", e));
            }
        }

        public void Eliminar(Empleado empleado)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(EmpleadoMapper.cadenaEliminar, BD());
                consulta.Parameters.AddWithValue("@id", empleado.Id);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                identityMap.Remove(empleado.Id);
            }
            catch (SqlException e)
            {
                throw new ApplicationException(String.Format("Imposible eliminar emplado.\n{0}", e));
            }
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
