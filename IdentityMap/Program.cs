using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Persona pepe = new Persona(0, "Pepe", "Cruz Llorente", 0);
            IdentityMap.anadirPersona(pepe);

            Persona persona = IdentityMap.getPersona(0);
            Console.WriteLine(persona.Nombre);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
