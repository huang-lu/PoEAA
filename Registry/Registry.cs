using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry
{
    class Registry
    {
        public static IDictionary<long, Persona> personas = new Dictionary<long, Persona>();

        public static Persona getPersona(long id)
        {
            if (personas.ContainsKey(id))
            {
                return personas[id];
            }
            return null;
        }

        public static void anadirPersona(Persona persona)
        {
            personas.Add(persona.PersonaId, persona);
        }

        public static void EliminarPersona(Persona persona)
        {
            personas.Remove(persona.PersonaId);
        }
    }
}
