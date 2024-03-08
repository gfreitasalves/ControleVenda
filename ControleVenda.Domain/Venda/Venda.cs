using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Venda.State;

namespace ControleVenda.Domain.Venda
{

    public class Venda
    {
        public Guid Id { get; set; }
        public Vendedor Vendedor { get; }
        public DateTime Data { get; }
        public List<ItemVenda> Itens { get; }
        public StatusVenda Status { get; internal set; }

        private readonly IStatusVendaState _statusVendaState;

        public Venda(Vendedor vendedor, List<ItemVenda> itens, StatusVenda status) :
            this(Guid.NewGuid(), vendedor, itens, status, DateTime.Now)
        {

        }

        private Venda(Guid id, Vendedor vendedor, List<ItemVenda> itens, StatusVenda status, DateTime data)
        {
            Id = id;
            Status = status;
            Vendedor = vendedor;
            Data = data;
            Itens = itens;

            _statusVendaState = VendaStateFactory.GetState(Status);
        }

        public void AtualizarStatus(StatusVenda novoStatus)
        {
            _statusVendaState.AtualizarStatus(this, novoStatus);
        }
    }
}
