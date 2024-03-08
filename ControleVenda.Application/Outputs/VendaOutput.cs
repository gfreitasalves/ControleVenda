using ControleVenda.Domain.Enums;

namespace ControleVenda.Application.Outputs
{
    public class VendaOutput(Guid id, string nomeVendedor, string cpfVendedor, DateTime data, string status, IEnumerable<ItemVendaOutput> itens)
    {
        public Guid Id { get; } = id;
        public string NomeVendedor { get; } = nomeVendedor;
        public string CpfVendedor { get; } = cpfVendedor;
        public DateTime Data { get; } = data;
        public string Status { get; } = status;
        public IEnumerable<ItemVendaOutput> Itens { get; } = itens;
    }
}
