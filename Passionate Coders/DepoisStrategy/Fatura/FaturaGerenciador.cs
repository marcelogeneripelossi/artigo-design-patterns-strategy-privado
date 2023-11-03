using Enumerações.Cliente;
using Negócios;

namespace DepoisStrategy
{
    internal class FaturaGerenciador
    {

        private IDictionary<CategoriaEnum, IClienteDescontoStrategy> estratégiasDeDescontoCategoriaDoCleinte;

        public FaturaGerenciador()
        {
            estratégiasDeDescontoCategoriaDoCleinte = new Dictionary<CategoriaEnum, IClienteDescontoStrategy>();
            estratégiasDeDescontoCategoriaDoCleinte.Add(CategoriaEnum.Bronze, new ClienteBronzeDescontoStrategy());
            estratégiasDeDescontoCategoriaDoCleinte.Add(CategoriaEnum.Novo, new ClienteNovoDescontoStrategy());
            estratégiasDeDescontoCategoriaDoCleinte.Add(CategoriaEnum.Ouro, new ClienteOuroDescontoStrategy());
            estratégiasDeDescontoCategoriaDoCleinte.Add(CategoriaEnum.Prata, new ClientePrataDescontoStrategy());

        }

        private decimal CalcularDescontoEstratégia(CategoriaEnum categoriaDoCliente)
        {
            return estratégiasDeDescontoCategoriaDoCleinte[categoriaDoCliente].CalcularDesconto();
        }

        public Fatura CriarFatura(Cliente cliente, int quantidadeItens, decimal preçoUnitário)
        {
            Fatura fatura;

            try
            {
                Console.WriteLine("\nUsando Strategy");

                fatura = new Fatura
                {
                    Cliente = cliente,
                    Linhas = new List<FaturaLinha>
                            {
                            new FaturaLinha { Quantidade = quantidadeItens, PreçoUnitário = preçoUnitário },
                },
                };


                fatura.DescontoPercentual = CalcularDescontoEstratégia(cliente.Categoria);

                return fatura;

            }
            catch (Exception)
            {
                throw new Exception("Erro na Criação da Fatura.");

            }

        }
    }
}
