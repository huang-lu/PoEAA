using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryObject
{
    class Program
    {
        static void Main(string[] args)
        {            
            ImprimirConsulta(Criterio.MayorQue("numeroDependientes", 1));
            ImprimirConsulta(new Criterio("LIKE", "nombre", "'%Juan%'"));

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void ImprimirConsulta(Criterio criterio)
        {
            QueryObject consulta = new QueryObject(typeof(Persona));
            consulta.AnadirCriterio(criterio);
            IList personas = consulta.Ejecutar();
            if (personas != null)
            {
                foreach (Persona persona in personas)
                {
                    Console.WriteLine(persona.ToString());
                }
            }
            else
            {
                Console.WriteLine("No se encontró ningún registro.\n");
            }
        }
    }
}
