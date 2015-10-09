using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class DataMap
    {
        private Type claseDominio;
        private string nombreTabla;
        private IList columnasMap = new ArrayList();

        public DataMap(Type clase, string nombreTabla)
        {
            claseDominio = clase;
            this.nombreTabla = nombreTabla;
        }

        public Type GetClaseDominio()
        {
            return claseDominio;
        }

        public void AnadirColumna(string nombreColumna, string tipo, string nombreAtributo)
        {
            columnasMap.Add(new ColumnMap(nombreColumna, nombreAtributo, this));
        }

        public string GetNombreTabla()
        {
            return nombreTabla;
        }

        public IList GetColumnas()
        {
            return columnasMap;
        }

        public string GetListaColumnas()
        {
            StringBuilder resultado = new StringBuilder(" ");
            foreach (ColumnMap item in columnasMap)
            {
                resultado.Append(", ");
                resultado.Append(item.GetNombreColumna());
            }
            return resultado.ToString();
        }

        public string GetListaActualizar()
        {
            StringBuilder resultado = new StringBuilder(" SET ");
            foreach (ColumnMap item in columnasMap)
            {
                resultado.Append(item.GetNombreColumna());
                resultado.Append(String.Format("=@{0},", item.GetNombreColumna()));
            }
            return resultado.ToString().TrimEnd(',');
        }

        public string GetListaInsertar()
        {
            StringBuilder resultado = new StringBuilder("");
            foreach (ColumnMap item in columnasMap)
            {
                resultado.Append(String.Format(",@{0}", item.GetNombreColumna()));
            }
            return resultado.ToString();
        }

        public string GetColumnaDelAtributo(string atributo)
        {
            foreach (ColumnMap item in columnasMap)
            {
                if (item.GetNombreAtributo().Equals(atributo))
                {
                    return item.GetNombreColumna();
                }
            }
            throw new ApplicationException(String.Format("No existe columnas para el atributo {0}", atributo));
        }
    }
}
