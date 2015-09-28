namespace UnitOfWork
{
    abstract class ObjetoDominio
    {
        abstract public int getId();

        protected void marcarNuevo()
        {
            UnitOfWork.getHilo().registrarNuevo(this);
        }

        protected void marcarLimpio()
        {
            UnitOfWork.getHilo().registrarLimpio(this);
        }

        protected void marcarModificado()
        {
            UnitOfWork.getHilo().registrarModificado(this);
        }

        protected void marcarEliminado()
        {
            UnitOfWork.getHilo().registrarEliminado(this);
        }
    }
}