//using AntesStrategy;
using DepoisStrategy;

using Enumerações.Cliente;
using Negócios;
using Repositório;

IList<Cliente> clientesCadastrados = ObterClientes();

#region Loop do Menu

while (true)
{
    try
    {
        ListarClientesCadastrados(clientesCadastrados);

        Console.WriteLine();

        Cliente? clienteSelecionado = SelecionarCliente(clientesCadastrados);

        int quantidade = InformarQuantidaDeItens();

        decimal preçoUnitário = InformarPreçoUnitário();


        #region Montagem da Fatura
        
        Console.WriteLine("\n-------------------------------------------------");

        Fatura fatura = new FaturaGerenciador()
                            .CriarFatura(
                                cliente: clienteSelecionado,
                                quantidadeItens: quantidade,
                                preçoUnitário: preçoUnitário
                            );


        Console.WriteLine($"\nFatura criada para o(a) \"Cliente {(CategoriaEnum)fatura.Cliente.Categoria}\" " +
                    $"{fatura.Cliente.Nome} " +
                    $"com o Valor sem Desconto de {fatura.ValorSemDesconto:C} " +
                    $"\nobtendo um Desconto de {fatura.DescontoPercentual:#0.##%} " +
                    $"({(fatura.DescontoPercentual * fatura.ValorSemDesconto):C2}) " +
                    $"com Valor de {fatura.ValorComDesconto:C} na Fatura.");

        #endregion Montagem da Fatura


        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Presione qualquer tecla para criar nova Fatura.");
        Console.ReadKey();
        Console.Clear();

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
};

#endregion Loop do Menu


#region Obtendo Clientes Cadastrados

static IList<Cliente> ObterClientes()
{

    var dataReader = new ClienteDataReader();
    var clientes = dataReader.ObterClientes();

    return clientes;
}

#endregion Obtendo Clientes Cadastrados


#region Lista dos Clientes cadastrados

static void ListarClientesCadastrados(IList<Cliente> clientes)
{

    Console.WriteLine("Lista de Clientes:");
    foreach (Cliente cliente in clientes)
    {
        Console.WriteLine($"[{cliente.Id}] {cliente.Nome} - Cliente {cliente.Categoria}");
    }

}

#endregion Lista dos Clientes cadastrados


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


#region Quantidade de itens

static int InformarQuantidaDeItens()
{

    int quantidade;

    do
    {
        Console.Write("Digite a Quantidade de Itens: ");
        int.TryParse(Console.ReadLine(), out quantidade);

    } while (quantidade == 0);

    return quantidade;
}

#endregion Quantidade de itens


#region Preço Unitário

static decimal InformarPreçoUnitário()
{

    decimal preçoUnitário;
    do
    {
        Console.Write("Digite o preço Unitário: ");
        decimal.TryParse(Console.ReadLine(), out preçoUnitário);

    } while (preçoUnitário == 0m);

    return preçoUnitário;
}

#endregion Preço Unitário

