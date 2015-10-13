using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMap
{
    class IdentityMap
    {
        static private IdentityMap instancia = new IdentityMap();
        private Dictionary<long, Persona> personas = new Dictionary<long, Persona>();

        private IdentityMap() { }

        public static void anadirPersona(Persona persona)
        {
            instancia.personas.Add(persona.Id, persona);
        }

        public static Persona getPersona(long id)
        {
            return instancia.personas[id];
        }
    }
}
