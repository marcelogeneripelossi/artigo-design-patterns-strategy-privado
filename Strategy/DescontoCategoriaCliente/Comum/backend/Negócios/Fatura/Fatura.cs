using Negócios;

namespace Negócios
{
    internal class Fatura
    {
        public Cliente Cliente { get; set; }

        public IList<FaturaProduto> FaturaProdutos { get; set; }

        public decimal ValorSemDesconto => FaturaProdutos.Sum(x => x.Quantidade * x.Produto.Preço);

        public decimal DescontoPercentual { get; set; }

        public decimal ValorComDesconto => ValorSemDesconto - (ValorSemDesconto * DescontoPercentual);
    }
}
