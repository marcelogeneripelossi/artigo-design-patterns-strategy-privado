namespace DepoisStrategy.Estratégias
{
    internal class ClienteBronzeDescontoStrategy : IClienteDescontoStrategy
    {
        public decimal CalcularDesconto()
        {
            return 0.02m;
        }

    }
}
