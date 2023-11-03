namespace DepoisStrategy.Estratégias
{
    internal class ClienteOuroDescontoStrategy : IClienteDescontoStrategy
    {

        public decimal CalcularDesconto()
        {
            return 0.06m;
        }
    }
}
