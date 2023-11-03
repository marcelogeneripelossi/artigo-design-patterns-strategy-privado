using Enumerações.Cliente;
using Negócios;

namespace AntesStrategy
{
    internal class FaturaGerenciador
    {
        private IDictionary<CategoriaEnum, decimal> estratégiasDeDescontoCategoriaDoCliente;

        public FaturaGerenciador()
        {
            estratégiasDeDescontoCategoriaDoCliente = new Dictionary<CategoriaEnum, decimal>();
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Novo, 0m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Bronze, 0.02m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Prata, 0.04m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Ouro, 0.06m);
        }

        public Fatura CriarFatura(Cliente cliente, IList<FaturaProduto> faturaProdutos)
        {

            Console.WriteLine("\nNão usando Strategy");

            Fatura fatura;

            try
            {

                fatura = new Fatura
                {
                    Cliente = cliente,
                    FaturaProdutos = faturaProdutos,
                };

                //switch (cliente.Categoria)
                //{
                //    case CategoriaEnum.Ouro:
                //        fatura.DescontoPercentual = 0.06m;

                //        break;

                //    case CategoriaEnum.Prata:
                //        fatura.DescontoPercentual = 0.04m;

                //        break;

                //    case CategoriaEnum.Bronze:
                //        fatura.DescontoPercentual = 0.02m;

                //        break;

                //    case CategoriaEnum.Novo:
                //        fatura.DescontoPercentual = 0m;

                //        break;

                //}

                fatura.DescontoPercentual = estratégiasDeDescontoCategoriaDoCliente[cliente.Categoria];

            }
            catch (Exception)
            {
                throw new Exception("Erro na Criação da Fatura.");

            }

            return fatura;

        }
    }
}
