using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataMapping
{
    class ObjetoDominio
    {
        public const long ID_SUSTITUTA = -1;
        public long id = ID_SUSTITUTA;

        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }
    }
}
