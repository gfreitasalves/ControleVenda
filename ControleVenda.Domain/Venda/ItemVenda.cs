using System.Runtime.CompilerServices;

namespace ControleVenda.Domain.Venda
{
    public class ItemVenda
    {
        public Guid Id { get; }
        public string Nome { get; }
        public decimal Valor { get; }
        public ItemVenda(string nome, decimal valor) :
            this(Guid.NewGuid(), nome, valor)
        {

        }

        private ItemVenda(Guid id, string nome, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser nulo ou vazio.", nameof(nome));
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero.", nameof(valor));

            Id = id;
            Nome = nome;
            Valor = valor;
        }        
    }
}
