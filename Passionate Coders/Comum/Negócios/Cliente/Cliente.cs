using Enumerações.Cliente;

namespace Negócios
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CategoriaEnum Categoria { get; set; }
    }
}
