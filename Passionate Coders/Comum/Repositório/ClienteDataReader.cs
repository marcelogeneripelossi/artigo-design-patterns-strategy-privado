using Enumerações.Cliente;
using Negócios;

namespace Repositório
{
    internal class ClienteDataReader
    {
        public IList<Cliente> ObterClientes()
        {
            return new List<Cliente> {
                new Cliente {
                Id = 1,
                Nome = "Marcelo",
                Categoria = CategoriaEnum.Novo,
                },
                new Cliente
                {
                    Id  = 2,
                    Nome = "Fernanda",
                Categoria = CategoriaEnum.Ouro,
                },
                new Cliente
                {
                    Id  = 3,
                    Nome = "Daniela",
                Categoria = CategoriaEnum.Ouro,
                },
                new Cliente
                {
                    Id  = 4,
                    Nome = "André",
                Categoria = CategoriaEnum.Prata,
                },
                new Cliente
                {
                    Id  = 5,
                    Nome = "Paulo",
                Categoria = CategoriaEnum.Bronze,
                }
            };
        }
    }
}
