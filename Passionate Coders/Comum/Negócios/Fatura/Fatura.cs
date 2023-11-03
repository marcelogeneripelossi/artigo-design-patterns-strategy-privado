namespace Negócios
{
    internal class Fatura
    {
        public Cliente Cliente { get; set; }

        public IList<FaturaLinha> Linhas { get; set; }

        public decimal ValorSemDesconto => Linhas.Sum(x => x.Quantidade * x.PreçoUnitário);

        public decimal DescontoPercentual { get; set; }

        public decimal ValorComDesconto => ValorSemDesconto - (ValorSemDesconto * DescontoPercentual);
    }
}
