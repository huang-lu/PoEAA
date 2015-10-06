using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTableInheritance
{
    abstract class Mapper
    {
        protected DataTable tabla
        {
            get
            {
                return gateway.Datos.Tables[NombreTabla];
            }
        }
        protected Gateway gateway;
        abstract protected string NombreTabla { get; }

        public Mapper(Gateway gateway)
        {
            this.gateway = gateway;
        }

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
            string filtro = String.Format("id = {0}", id);
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

        protected ObjetoDominio Buscar(DataRow fila)
        {
            ObjetoDominio objeto = CrearObjetoDominio();
            Cargar(objeto, fila);
            return objeto;
        }

        abstract protected ObjetoDominio CrearObjetoDominio();
        protected virtual void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            objeto.id = (int)fila["Id"];
        }

        public virtual void Actualizar(ObjetoDominio objeto)
        {
            Guardar(objeto, BuscarFila(objeto.id));
            gateway.guardarABd(tabla.TableName);
        }

        public virtual long Insertar(ObjetoDominio objeto)
        {
            DataRow fila = tabla.NewRow();
            objeto.id = gateway.siguienteId(tabla.TableName);
            fila["Id"] = objeto.id;
            Guardar(objeto, fila);
            tabla.Rows.Add(fila);
            gateway.guardarABd(tabla.TableName);
            return objeto.id;
        }

        abstract protected void Guardar(ObjetoDominio objeto, DataRow fila);
    }
}
