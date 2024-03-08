namespace ControleVenda.Application.Outputs
{
    public class ItemVendaOutput(Guid id, string nome, decimal valor)
    {
        public Guid Id { get; } = id;
        public string Nome { get; } = nome;
        public decimal Valor { get; } = valor;
    }
}