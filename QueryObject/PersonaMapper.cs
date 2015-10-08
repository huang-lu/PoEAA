using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryObject
{
    class PersonaMapper : Mapper
    {
        protected override void CargarDataMap()
        {
            dataMap = new DataMap(typeof(Persona), "Persona");
            dataMap.AnadirColumna("Nombre", "varchar", "nombre");
            dataMap.AnadirColumna("Apellidos", "varchar", "apellidos");
            dataMap.AnadirColumna("NumeroDependientes", "int", "numeroDependientes");
        }

        protected override string GetColumnaId()
        {
            return "PersonaId";
        }
    }
}
