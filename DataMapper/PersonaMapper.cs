using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    class PersonaMapper : MapperAbstracto
    {
        protected override string cadenaBuscar()
        {
            return "SELECT * FROM Persona WHERE PersonaId = @id";
        }

        public Persona buscar(int PersonaId)
        {
            return (Persona)buscarAbstracto(PersonaId);
        }

        protected override ObjetoDominio hacerCarga(int id, SqlDataReader fila)
        {
            string nombre = Convert.ToString(fila["Nombre"]);
            string apellidos = Convert.ToString(fila["Apellidos"]);
            int numDependientes = Convert.ToInt32(fila["numeroDependientes"]);
            return new Persona(id, nombre, apellidos, numDependientes);
        }
    }
}
