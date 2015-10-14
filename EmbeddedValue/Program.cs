using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class Program
    {
        static void Main(string[] args)
        {
            Empleado pepe = EmpleadoMapper.instancia.Buscar(1);
            Console.WriteLine(pepe.Nombre);
            Console.WriteLine("\tPeriodo: {0} - {1}", pepe.Periodo.Inicial, pepe.Periodo.Final);
            Console.WriteLine("\tSalario: {0} Moneda: {1}", pepe.Salario.Importe, pepe.Salario.Moneda);

            Empleado juan = EmpleadoMapper.instancia.Buscar(2);
            juan.Nombre = "Pedro";
            EmpleadoMapper.instancia.Actualizar(juan);

            EmpleadoMapper.instancia.Eliminar(juan);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
