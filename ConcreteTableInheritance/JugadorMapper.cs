using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    class JugadorMapper : Mapper
    {
        private BowlerMapper bowlerMapper;
        private CricketerMapper cricketerMapper;
        private FutbolistaMapper futbolistaMapper;

        public JugadorMapper(Gateway gatW) : base(gatW)
        {
            bowlerMapper = new BowlerMapper(gateway);
            cricketerMapper = new CricketerMapper(gateway);
            futbolistaMapper = new FutbolistaMapper(gateway);
        }

        public override string NombreTabla
        {
            get
            {
                return "Jugador";
            }
        }

        public Jugador Buscar(long id)
        {
            Jugador resultado;
            resultado = futbolistaMapper.Buscar(id);
            if (resultado != null)
            {
                return resultado;
            }
            resultado = bowlerMapper.Buscar(id);
            if (resultado != null)
            {
                return resultado;
            }
            resultado = cricketerMapper.Buscar(id);
            if (resultado != null)
            {
                return resultado;
            }
            return null;         
        }

        public override void Actualizar(ObjetoDominio objeto)
        {
            MapeoCon(objeto).Actualizar(objeto);
        }

        private Mapper MapeoCon(ObjetoDominio objeto)
        {
            if (objeto is Futbolista)
            {
                return futbolistaMapper;
            }
            else if (objeto is Bowler)
            {
                return bowlerMapper;
            }
            else
            {
                return cricketerMapper;
            }
            throw new Exception("Mapper no disponible");
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            throw new NotImplementedException();
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            throw new NotImplementedException();
        }
    }
}
