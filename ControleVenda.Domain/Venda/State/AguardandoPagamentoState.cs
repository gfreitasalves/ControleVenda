using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;

namespace ControleVenda.Domain.Venda.State
{
    public class AguardandoPagamentoState : IStatusVendaState
    {
        public void AtualizarStatus(Venda venda, StatusVenda novoStatus)
        {
            if (novoStatus == StatusVenda.PagamentoAprovado || novoStatus == StatusVenda.Cancelada)
                venda.Status = novoStatus;
            else
                throw new RegraNegocioException($"Transição de status inválida de:'{venda.Status}' para:'{novoStatus}'.");
        }
    }

}
