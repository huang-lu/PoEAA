using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDataGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonaGateway persona = new PersonaGateway();
            SqlDataReader resultado;
            persona.Insertar("Juan Luis", "Ruiz Criado", 0);
            persona.Insertar("Pepito", "Jimenez Pérez", 1);
            persona.Insertar("Juan Pedro", "Ortiz Carmona", 2);
            persona.Insertar("José Juan", "Ortiz Carmona", 2);

            resultado = persona.BuscarTodos();
            imprimirBusqueda(resultado, "Resultado BuscarTodos()");

            resultado = persona.BuscarWhere("Nombre LIKE '%Juan%'");
            imprimirBusqueda(resultado, "Resultado BuscarWhere(Nombre LIKE'%Juan%')");

            resultado = persona.BuscarPorApellidos("Ortiz");
            imprimirBusqueda(resultado, "Resultado BuscarPorApellidos('Ortiz')");

            Object[] fila = persona.BuscarFila(1);
            Console.WriteLine("Resultado de BuscarFila(1)");
            foreach (var item in fila)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void imprimirBusqueda(SqlDataReader resultado, string mensaje)
        {
            Console.WriteLine(mensaje);
            while (resultado.Read())
            {
                Object[] columnas = new Object[resultado.FieldCount];
                resultado.GetValues(columnas);
                foreach (var atributo in columnas)
                {
                    Console.Write("{0} ", atributo);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
