using AntesStrategy;
//using DepoisStrategy;

using Enumerações.Cliente;
using Negócios;
using Repositório;

try
{

    IList<Cliente> clientesCadastrados = ObterClientes();
    IList<Produto> produtosDisponíveis = ObterProdutos();

    #region Loop do Menu

    while (true)
    {

        ListarClientesCadastrados(clientesCadastrados);

        Cliente? clienteSelecionado = SelecionarCliente(clientesCadastrados);


        #region Montar Carrinho de Compras

        IList<FaturaProduto> produtosComprados = new List<FaturaProduto>();
        Produto? produtoSelecionado = null;

        while (true)
        {

            Console.WriteLine();
            ListarProdutosDisponíveis(produtosDisponíveis);

            int produtoID = InformarProdutoDesejado(produtosDisponíveis, ref produtoSelecionado);

            if (produtoID == 0 && produtosComprados.Count == 0)
                throw new Exception("Saindo do Sistema.");

            if (produtoID == 0)
                break;

            ushort quantidade = InformarQuantidadeDesejada();

            produtosComprados.Add(new FaturaProduto { Produto = produtoSelecionado, Quantidade = quantidade });

            produtosDisponíveis.Remove(produtoSelecionado);
            if (produtosDisponíveis.Count == 0)
                break;

        }

        #endregion Montar Carrinho de Compras


        Console.WriteLine("\n-------------------------------------------------");


        #region Montar Fatura

        Fatura fatura = new FaturaGerenciador()
                            .CriarFatura(
                                cliente: clienteSelecionado,
                                faturaProdutos: produtosComprados
                            );

        #endregion Montar Fatura


        #region Exibir Fatura

        Console.WriteLine($"\n\nFatura criada para o(a) \"Cliente {(CategoriaEnum)fatura.Cliente.Categoria}\" " +
                            $"{fatura.Cliente.Nome} " +
                            $"com o Valor sem Desconto de {fatura.ValorSemDesconto:C} " +
                            $"\nobtendo um Desconto de {fatura.DescontoPercentual:#0.##%} " +
                            $"({(fatura.DescontoPercentual * fatura.ValorSemDesconto):C2}) " +
                            $"com Valor de {fatura.ValorComDesconto:C} na Fatura.\n");


        // Linhas da Fatura
        foreach (FaturaProduto linhaFatura in fatura.FaturaProdutos)
        {
            LinhaFaturaParâmetros parâmetrosLinha = new LinhaFaturaParâmetros
            {
                Nome = linhaFatura.Produto.Nome,
                Quantidade = linhaFatura.Quantidade,
                Valor = linhaFatura.Produto.Preço,
                ValorTotal = (linhaFatura.Produto.Preço * linhaFatura.Quantidade),
            };

            LinhaFatura(parâmetrosLinha);
        }

        // Linha da Fatura - Total Sem Desconto
        Console.WriteLine(string.Empty.PadLeft(9) + "-------------------------------------------------");

        ushort quantidadeTotalProdutos = (ushort)fatura.FaturaProdutos.Sum(x => x.Quantidade);
        ushort valorTotalProdutos = (ushort)fatura.FaturaProdutos.Sum(x => x.Produto.Preço);

        LinhaFaturaParâmetros parâmetros = new LinhaFaturaParâmetros
        {
            Nome = "produto(s)",
            Quantidade = quantidadeTotalProdutos,
            Valor = valorTotalProdutos,
            ValorTotal = fatura.ValorSemDesconto,
            TipoDaLinha = LinhaFaturaTipoEnum.TotalSemDesconto
        };

        LinhaFatura(parâmetros);

        // Linha da Fatura - Total Com Desconto
        Console.WriteLine(string.Empty.PadLeft(9) + "-------------------------------------------------");

        parâmetros = new LinhaFaturaParâmetros
        {
            Nome = "produto(s)",
            Quantidade = quantidadeTotalProdutos,
            Valor = valorTotalProdutos,
            ValorTotal = fatura.ValorComDesconto,
            TipoDaLinha = LinhaFaturaTipoEnum.TotalComDesconto
        };

        LinhaFatura(parâmetros);

        #endregion Exibir Fatura


        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Presione qualquer tecla para criar nova Fatura.");
        Console.ReadKey();
        Console.Clear();

        produtosDisponíveis = ObterProdutos();

    };

    #endregion Loop do Menu

}
catch (Exception exceção)
{
    Console.WriteLine();
    Console.WriteLine(exceção.Message);
    Console.WriteLine("Sistema encerrado.");
    Console.WriteLine("Pressione qualquer tecla para continuar.");
    Console.ReadKey();
    Environment.Exit(0);

}


#region Obtendo Clientes Cadastrados

static IList<Cliente> ObterClientes()
{
    var dataReaderClientes = new ClienteDataReader();
    var clientesCadastrados = dataReaderClientes.ObterClientes();
    return clientesCadastrados;
}

#endregion Obtendo Clientes Cadastrados


#region Obtendo Produtos Cadastrados
static IList<Produto> ObterProdutos()
{
    var dataReaderProdutos = new ProdutoDataReader();
    var produtosCadastrados = dataReaderProdutos.ObterProdutos();
    return produtosCadastrados;
}


#endregion Obtendo Produtos Cadastrados


#region Lista dos Clientes cadastrados

static void ListarClientesCadastrados(IList<Cliente> clientesCadastrados)
{
    Console.WriteLine("Lista de Clientes:");
    foreach (Cliente cliente in clientesCadastrados)
    {
        Console.WriteLine($"[{cliente.Id}] {cliente.Nome} - Cliente {cliente.Categoria}");
    }

    Console.WriteLine();
}

#endregion Lista dos Clientes cadastrados


#region Lista dos Produtos cadastrados

static void ListarProdutosDisponíveis(IList<Produto> produtosCadastrados)
{

    Console.WriteLine("Lista dos Produtos cadastrados:");
    foreach (Produto produto in produtosCadastrados)
    {
        Console.WriteLine($"[{produto.Id}] {produto.Nome} - R$ {produto.Preço:F2}");
    }

    Console.WriteLine();

}

#endregion Lista dos Produtos cadastrados


#region Escolha do Cliente desejado

static Cliente SelecionarCliente(IList<Cliente> clientesCadastrados)
{

    int clienteID = 0;
    Cliente? clienteSelecionado = null;

    do
    {
        Console.Write("Digite o ID do Cliente: ");
        int.TryParse(Console.ReadLine(), out clienteID);

        if (clienteID == 0)
            throw new Exception("Saindo do Sistema.");

        clienteSelecionado = clientesCadastrados.FirstOrDefault(x => x.Id == clienteID);

        if (clienteSelecionado == null)
            Console.WriteLine("Cliente não encontrado.\n");

    } while (clienteSelecionado == null);


    return clienteSelecionado;
}

#endregion Escolha do Cliente desejado


#region Escolha do(s) Produto(s) desejado(s)

static int InformarProdutoDesejado(IList<Produto> produtosDisponíveis, ref Produto? produtoSelecionado)
{

    int produtoID = 0;

    do
    {
        Console.Write("Digite o ID do Produto desejado (0 ou <ENTER> para Avançar): ");
        int.TryParse(Console.ReadLine(), out produtoID);

        if (produtoID == 0)
            break;

        produtoSelecionado = produtosDisponíveis.FirstOrDefault(x => x.Id == produtoID);

        if (produtoSelecionado == null)
            Console.WriteLine("Produto não encontrado.\n");

    } while (produtoSelecionado == null);


    return produtoID;
}

#endregion Escolha do(s) Produto(s) desejado(s)


#region Quantidade de itens

static ushort InformarQuantidadeDesejada()
{

    ushort quantidade = 0;
    do
    {
        Console.Write("Digite a Quantidade de Itens: ");
        ushort.TryParse(Console.ReadLine(), out quantidade);

    } while (quantidade == 0);

    return quantidade;
}

#endregion Quantidade de itens


#region Relacionados à Linha da Fatura

static void LinhaFatura(LinhaFaturaParâmetros parâmetros)
{
    Console.WriteLine($"" +
                    $"{(parâmetros.Quantidade).ToString("N0").PadLeft(10)}" +
                    $" {parâmetros.Nome.PadRight(10)}" +
                    $" a {parâmetros.Valor.ToString("C2").PadLeft(10)}" +
                    $" = " +
                    $" {parâmetros.ValorTotal.ToString("C2").PadLeft(10)}" +
                    $" {(
                            parâmetros.TipoDaLinha == LinhaFaturaTipoEnum.TotalSemDesconto ?
                            " sem Desconto" :
                            parâmetros.TipoDaLinha == LinhaFaturaTipoEnum.TotalComDesconto ?
                            " com Desconto" :
                            String.Empty
                        )}"
    );
}

internal struct LinhaFaturaParâmetros
{
    public string Nome { get; set; }
    public ushort Quantidade { get; set; }
    public LinhaFaturaTipoEnum TipoDaLinha { get; set; }
    public decimal Valor { get; set; }
    public decimal ValorTotal { get; set; }
}

internal enum LinhaFaturaTipoEnum
{
    TotalComDesconto = 1,
    TotalSemDesconto = 2
}

#endregion Relacionados à Linha da Fatura




