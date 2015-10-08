using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryObject
{
    class Criterio
    {
        private string operadorSql;
        protected string atributo;
        protected Object valor;

        public Criterio(string operadorSql, string atributo, Object valor)
        {
            this.operadorSql = operadorSql;
            this.atributo = atributo;
            this.valor = valor;
        }

        public static Criterio MayorQue(string atributo, Object valor)
        {
            return new Criterio(">", atributo, valor);
        }

        public string GenerarSql(DataMap dataMap)
        {
            return dataMap.GetColumnaDelAtributo(atributo) + " " + operadorSql + " " + valor;
        }
    }
}
