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
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Bronze, 0.02m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Novo, 0m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Ouro, 0.06m);
            estratégiasDeDescontoCategoriaDoCliente.Add(CategoriaEnum.Prata, 0.04m);
        }

        public Fatura CriarFatura(Cliente cliente, int quantidadeItens, decimal preçoUnitário)
        {

            Console.WriteLine("\nNão usando Strategy");

            Fatura fatura;

            try
            {
                fatura = new Fatura
                {
                    Cliente = cliente,
                    Linhas = new List<FaturaLinha>
                {
                    new FaturaLinha { Quantidade = quantidadeItens, PreçoUnitário = preçoUnitário },
                },
                };

                fatura.DescontoPercentual = estratégiasDeDescontoCategoriaDoCliente[cliente.Categoria];

                //switch (cliente.Categoria)
                //{
                //    case CategoriaEnum.Bronze:

                //        break;

                //    case CategoriaEnum.Novo:
                //        fatura.DescontoPercentual = 0m;

                //        break;

                //    case CategoriaEnum.Ouro:
                //        fatura.DescontoPercentual = 0.06m;

                //        break;

                //    case CategoriaEnum.Prata:
                //        fatura.DescontoPercentual = 0.04m;

                //        break;

                //    default:
                //        fatura.DescontoPercentual = 0.0m;

                //        break;
                //}
            }
            catch (Exception)
            {
                throw new Exception("Erro na Criação da Fatura.");

            }

            return fatura;
        }

    }
}
