namespace DepoisStrategy
{
    internal class ClienteOuroDescontoStrategy : IClienteDescontoStrategy
    {

        public decimal CalcularDesconto()
        {
            return 0.06m;
        }
    }
}
