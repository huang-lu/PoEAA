using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    abstract class Mapper
    {
        public Gateway gateway;
        private IDictionary identityMap = new Hashtable();

        public Mapper(Gateway gateway)
        {
            this.gateway = gateway;
        }

        private DataTable tabla
        {
            get { return gateway.Datos.Tables[NombreTabla]; }
        }
        abstract public string NombreTabla { get; }

        public ObjetoDominio BuscarAbstracto(long id)
        {
            DataRow fila = BuscarFila(id);
            if (fila == null)
            {
                return null;
            }
            else
            {
                ObjetoDominio resultado = CrearObjetoDominio();
                Cargar(resultado, fila);
                return resultado;
            }
        }

        private DataRow BuscarFila(long id)
        {
            string filtro = String.Format("Id = {0}", id);
            DataRow[] filas = tabla.Select(filtro);
            if (filas.Length == 0)
            {
                return null;
            }
            else
            {
                return filas[0];
            }
        }

        protected abstract ObjetoDominio CrearObjetoDominio();
        protected virtual void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            objeto.id = (int)fila["Id"];
        }

        public virtual void Actualizar(ObjetoDominio objeto)
        {
            Guardar(objeto, BuscarFila(objeto.id));
        }
        abstract protected void Guardar(ObjetoDominio objeto, DataRow fila);
    }
}
