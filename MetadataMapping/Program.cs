using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonaMapper personaMappper = new PersonaMapper();
            Persona persona = (Persona)personaMappper.BuscarObjeto(7);
            persona.Nombre = "Antonia";
            personaMappper.Actualizar(persona);
            personaMappper.Eliminar(persona);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
