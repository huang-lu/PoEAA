using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTableInheritance
{
    class BowlerMapper : CricketerMapper
    {
        public const string TIPO = "B";

        public BowlerMapper(Gateway gateway) : base(gateway) { }

        public override string Tipo
        {
            get { return TIPO; }
        }

        public Bowler Buscar(long id)
        {
            return (Bowler)base.Buscar(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Bowler bowler = (Bowler)objeto;
            bowler.mediaBowling = (int)fila["MediaBowling"];
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            base.Guardar(objeto, fila);
            Bowler bowler = (Bowler)objeto;
            fila["MediaBowling"] = bowler.mediaBowling;
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Bowler();
        }
    }
}
