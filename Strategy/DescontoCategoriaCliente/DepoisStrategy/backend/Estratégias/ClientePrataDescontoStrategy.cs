namespace DepoisStrategy.Estratégias
{
    internal class ClientePrataDescontoStrategy : IClienteDescontoStrategy
    {

        public decimal CalcularDesconto()
        {
            return 0.06m;
        }
    }
}
