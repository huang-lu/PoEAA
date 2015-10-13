using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry
{
    class Program
    {
        static void Main(string[] args)
        {
            Persona persona = Persona.Buscar(7);

            if (persona != null)
            {
                Console.WriteLine(persona.Nombre);

                persona.Nombre = "Paco";
                Console.WriteLine(persona.Nombre);
                persona.Actualizar();

                Console.WriteLine("La desgravación es {0} porque tiene a su cargo {1} personas",
                                    persona.getDesgravacion(), persona.NumeroDependientes);
            }
            else
            {
                Console.WriteLine("No se ha encontrado a ninguna persona con ese ID.");
            }
            

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
