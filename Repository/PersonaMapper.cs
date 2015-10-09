using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class PersonaMapper : Mapper
    {
        protected override void CargarDataMap()
        {
            dataMap = new DataMap(typeof(Persona), "Persona");
            dataMap.AnadirColumna("Nombre", "varchar", "nombre");
            dataMap.AnadirColumna("Apellidos", "varchar", "apellidos");
            dataMap.AnadirColumna("NumeroDependientes", "int", "numeroDependientes");
            dataMap.AnadirColumna("Benefactor", "int", "benefactor");
        }

        protected override string GetColumnaId()
        {
            return "PersonaId";
        }
    }
}
