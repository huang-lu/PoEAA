using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMap
{
    class ObjetoDominio
    {
        protected long id;

        public long Id { get { return id; } set { id = value; } }

        public override int GetHashCode()
        {
            int primo = 31;
            int resultado = 1;
            resultado = primo * resultado + (int)(id ^ (id >> 32));
            return resultado;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            ObjetoDominio otro = (ObjetoDominio)obj;
            if (Id != otro.Id)
            {
                return false;
            }
            return true;
        }
    }
}
