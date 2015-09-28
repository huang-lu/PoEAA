using System;
using System.Collections;
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

        public Persona Buscar(int PersonaId)
        {
            return (Persona)buscarAbstracto(PersonaId);
        }

        public ArrayList BuscarPorApellidos(string patron)
        {
            return BuscarCualquier(new BuscarPorApellidos(patron));
        }

        protected override ObjetoDominio hacerCarga(int id, SqlDataReader fila)
        {
            string nombre = Convert.ToString(fila["Nombre"]);
            string apellidos = Convert.ToString(fila["Apellidos"]);
            int numDependientes = Convert.ToInt32(fila["numeroDependientes"]);
            return new Persona(id, nombre, apellidos, numDependientes);
        }        
    }

    class BuscarPorApellidos : Consulta
    {
        private string apellidos;

        public BuscarPorApellidos(string apellidos)
        {
            this.apellidos = apellidos;
        }

        public Hashtable Parametros()
        {
            Hashtable resultado = new Hashtable();
            resultado.Add("@apellidos", apellidos);
            return resultado;
        }

        public string Sql()
        {
            return "SELECT * FROM Persona WHERE UPPER(Apellidos) LIKE UPPER(@apellidos)";
        }
    }
}
