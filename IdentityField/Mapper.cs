using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityField
{
    abstract class Mapper
    {
        protected DataTable tabla;
        protected ObjetoDominio BuscarAbstracto(long id)
        {
            DataRow fila = BuscarFila(id);
            if (fila == null)
            {
                return null;
            }
            else
            {
                return Buscar(fila);
            }
        }       

        protected DataRow BuscarFila(long id)
        {
            string filtro = Filtro(id);
            DataRow[] resultados = tabla.Select(filtro);
            if (resultados.Length == 0)
            {
                return null;
            }
            else
            {
                return resultados[0];
            }
        }

        protected abstract string Filtro(long id);

        private ObjetoDominio Buscar(DataRow fila)
        {
            ObjetoDominio resultado = CrearObjetoDominio();
            Cargar(resultado, fila);
            return resultado;
        }

        abstract protected void Cargar(ObjetoDominio objeto, DataRow fila);

        abstract protected ObjetoDominio CrearObjetoDominio();

        public virtual long Insertar(ObjetoDominio objeto)
        {
            DataRow fila = tabla.NewRow();
            objeto.id = getSiguienteId();            
            Guardar(objeto, fila);
            tabla.Rows.Add(fila);
            return objeto.id;
        }

        abstract protected long getSiguienteId();

        abstract protected void Guardar(ObjetoDominio objeto, DataRow fila);
    }
}
