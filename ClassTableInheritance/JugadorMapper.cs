﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    class JugadorMapper : Mapper
    {
        private BowlerMapper bowlerMapper;
        private CricketerMapper cricketerMapper;
        private FutbolistaMapper futbolistaMapper;

        protected static string NombreTabla = "Jugador";

        public JugadorMapper(Gateway gatW) : base(gatW)
        {
            bowlerMapper = new BowlerMapper(gateway);
            cricketerMapper = new CricketerMapper(gateway);
            futbolistaMapper = new FutbolistaMapper(gateway);
        }       

        public Jugador Buscar(long id)
        {
            DataRow fila = BuscarFila(id, Tabla(NombreTabla));
            if (fila == null)
            {
                return null;
            }
            else
            {
                string tipo = (string)fila["Tipo"];
                switch (tipo)
                {
                    case BowlerMapper.TIPO:
                        return (Jugador)bowlerMapper.Buscar(id);
                    case CricketerMapper.TIPO:
                        return (Jugador)cricketerMapper.Buscar(id);
                    case FutbolistaMapper.TIPO:
                        return (Futbolista)futbolistaMapper.Buscar(id);
                    default:
                        throw new Exception("Tipo desconocido");
                }
            }
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            throw new NotImplementedException();
        }

        public override void Actualizar(ObjetoDominio objeto)
        {
            MapeoCon(objeto).Actualizar(objeto);
        }

        public override long Insertar(ObjetoDominio objeto)
        {
            return MapeoCon(objeto).Insertar(objeto);
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

        protected override void Guardar(ObjetoDominio objeto)
        {
            throw new NotImplementedException();
        }

        protected override void Cargar(ObjetoDominio objeto)
        {
            throw new NotImplementedException();
        }

        protected override void AnadirFila(ObjetoDominio objeto)
        {
            throw new NotImplementedException();
        }
    }
}
