namespace DepoisStrategy
{
    internal class ClienteBronzeDescontoStrategy : IClienteDescontoStrategy
    {
        public decimal CalcularDesconto()
        {
            return 0.02m;
        }

    }
}
