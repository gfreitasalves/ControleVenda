using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;

namespace ControleVenda.Domain.Venda.State
{
    public class EntregueState : IStatusVendaState
    {
        public void AtualizarStatus(Venda venda, StatusVenda novoStatus)
        {
            throw new RegraNegocioException($"Transição de status inválida de:'{venda.Status}' para:'{novoStatus}'.");
        }
    }

}
