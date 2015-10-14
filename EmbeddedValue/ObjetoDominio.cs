using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class ObjetoDominio
    {
        private long id;

        public ObjetoDominio(long id)
        {
            this.id = id;
        }

        public long Id { get { return id; } set { value = id; } }
    }
}
