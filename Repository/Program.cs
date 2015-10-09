using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Program
    {
        static void Main(string[] args)
        {
            Repositorio repositorio = new RepositorioPersona();
            IList personas = repositorio.Coincidencias(Criterio.Contiene("nombre", "'%Juan%'"));
            foreach (Persona persona in personas)
            {
                Console.WriteLine("Las personas dependientes de {0} son {1}:", persona.Nombre, persona.NumeroDependientes);
                IList dependientes = persona.Dependientes();
                if (dependientes != null)
                {
                    foreach (Persona dependiente in dependientes)
                    {
                        Console.WriteLine("\t{0}", dependiente.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\tNo tiene personas dependientes");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
