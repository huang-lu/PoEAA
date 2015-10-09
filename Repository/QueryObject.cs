using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class QueryObject
    {
        private Type tipo;
        private IList criterio = new ArrayList();

        public QueryObject(Type tipo)
        {
            this.tipo = tipo;
        }

        public void AnadirCriterio(Criterio criterio)
        {
            this.criterio.Add(criterio);
        }

        public IList Ejecutar()
        {
            return (IList)Mapper.GetMapper(tipo).BuscarObjetoWhere(GenerarClausulaWhere());
        }

        private string GenerarClausulaWhere()
        {
            StringBuilder resultado = new StringBuilder();
            foreach (Criterio item in criterio)
            {
                if (resultado.Length != 0)
                {
                    resultado.Append(" AND ");
                }
                resultado.Append(item.GenerarSql(Mapper.GetMapper(tipo).GetDataMap()));
            }
            return resultado.ToString();
        }
    }
}
