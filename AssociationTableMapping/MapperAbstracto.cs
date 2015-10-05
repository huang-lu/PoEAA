using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationTableMapping
{
    abstract class MapperAbstracto
    {
        protected DataTable tabla
        {
            get
            {
                return dsh.Datos.Tables[NombreTabla];
            }
        }
        public DataSetHolder dsh = new DataSetHolder();
        abstract protected String NombreTabla { get; }
        private IDictionary identityMap = new Hashtable();

        protected ObjetoDominio BuscarAbstracto(long id)
        {
            DataRow fila = BuscarFila(id);
            if ( fila == null)
            {
                return null;
            }
            else
            {
                return Cargar(fila);
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

        abstract protected string Filtro(long id);
        protected ObjetoDominio Cargar(DataRow fila)
        {
            long id = (int)fila[0];
            if (identityMap[id] != null)
            {
                return (ObjetoDominio)identityMap[id];
            }
            else
            {
                ObjetoDominio resultado = CrearObjetoDominio();
                resultado.id = id;
                identityMap.Add(resultado.id, resultado);
                HacerCarga(resultado, fila);
                return resultado;
            }
        }

        public virtual void Actualizar(ObjetoDominio objeto)
        {
            Guardar(objeto, BuscarFila(objeto.id));
        }

        public static HabilidadMapper Habilidad
        {
            get
            {
                return new HabilidadMapper();
            }
        }

        abstract protected void Guardar(ObjetoDominio objeto, DataRow fila);
        abstract protected ObjetoDominio CrearObjetoDominio();
        abstract protected void HacerCarga(ObjetoDominio objeto, DataRow fila);
    }
}
