using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class RepositorioPersona : Repositorio
    {
        public override IList Coincidencias(Criterio criterio)
        {
            QueryObject consulta = new QueryObject(typeof(Persona));
            consulta.AnadirCriterio(criterio);
            return consulta.Ejecutar();
        }

        public IList DependientesDe(Persona persona)
        {
            Criterio criterio = Criterio.Igual("benefactor", persona.GetId());
            return Coincidencias(criterio);
        }
    }
}
