using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;

namespace ControleVenda.Domain.Venda.State
{
    public class EnviadoParaTransportadoraState : IStatusVendaState
    {
        public void AtualizarStatus(Venda venda, StatusVenda novoStatus)
        {
            if (novoStatus == StatusVenda.Entregue)
                venda.Status = novoStatus;
            else
                throw new RegraNegocioException($"Transição de status inválida de:'{venda.Status}' para:'{novoStatus}'.");
        }
    }


}
