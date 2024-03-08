using ControleVenda.Domain.Enums;

namespace ControleVenda.Domain.Venda.State
{
    public interface IStatusVendaState
    {
        void AtualizarStatus(Venda venda, StatusVenda novoStatus);
    }


}
