using Negócios;

namespace Repositório
{
    internal class ProdutoDataReader
    {
        public IList<Produto> ObterProdutos()
        {
            return new List<Produto> {
                new Produto {
                Id = 1,
                Nome = "Calça",
                Preço = 10,
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Tênis",
                    Preço = 20,
                },
                new Produto
                {
                    Id = 3,
                    Nome = "Cinto",
                    Preço = 5,
                },
                new Produto
                {
                    Id = 4,
                    Nome = "Pulseira",
                    Preço = 1,
                },
                new Produto
                {
                    Id = 5,
                    Nome = "Meia",
                    Preço = 5,
                }
            };
        }
    }
}
