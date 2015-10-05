using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationTableMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            EmpleadoMapper empMap = new EmpleadoMapper();
            Empleado emp = empMap.Buscar(2);
            foreach (Habilidad hab in emp.Habilidades)
            {
                Console.WriteLine(hab.Nombre);
            }
            Console.WriteLine();

            HabilidadMapper habiMap = new HabilidadMapper();
            Habilidad habilidad = habiMap.Buscar(5);
            habilidad.Nombre = "Planificación";
            habiMap.Actualizar(habilidad);
            emp.AnadirHabilidad(habilidad);
            empMap.Actualizar(emp);
            foreach (Habilidad hab in emp.Habilidades)
            {
                Console.WriteLine(hab.Nombre);
            }
            emp.EliminarHabilidad(habilidad);
            empMap.Actualizar(emp);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
