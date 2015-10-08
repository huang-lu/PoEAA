using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataMapping
{
    class ColumnMap
    {
        private string nombreColumna;
        private string nombreAtributo;
        private FieldInfo atributo;
        private DataMap dataMap;

        public ColumnMap(string nombreColumna, string nombreAtributo, DataMap dataMap)
        {
            this.nombreColumna = nombreColumna;
            this.nombreAtributo = nombreAtributo;
            this.dataMap = dataMap;
            IniciarAtributo();
        }

        public string GetNombreColumna()
        {
            return nombreColumna;
        }

        public void SetAtributo(Object objeto, Object valor)
        {
            try
            {
                atributo.SetValue(objeto, valor);
            }
            catch (Exception)
            {
                throw new ApplicationException("Imposible asignar atributo");
            }
        }

        public string GetNombreAtributo()
        {
            return nombreAtributo;
        }

        public Object GetValor(Object objeto)
        {
            try
            {
                return atributo.GetValue(objeto);
            }
            catch (Exception)
            {
                throw new ApplicationException("Imposible obtener valor");
            }
        }

        private void IniciarAtributo()
        {
            try
            {
                atributo = dataMap.GetClaseDominio().GetField(GetNombreAtributo(), BindingFlags.NonPublic | BindingFlags.Instance);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Imposible iniciar el campo: " + nombreAtributo, e);
            }
        }        
    }
}
