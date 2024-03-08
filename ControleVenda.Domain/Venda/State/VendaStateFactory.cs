using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;

namespace ControleVenda.Domain.Venda.State
{
    public static class VendaStateFactory
    {
        public static IStatusVendaState GetState(StatusVenda status)
        {
            switch (status)
            {
                case StatusVenda.AguardandoPagamento:
                    return new AguardandoPagamentoState();
                case StatusVenda.PagamentoAprovado:
                    return new PagamentoAprovadoState();
                case StatusVenda.EnviadoParaTransportadora:
                    return new EnviadoParaTransportadoraState();
                case StatusVenda.Entregue:
                    return new EntregueState();
                case StatusVenda.Cancelada:
                    return new CanceladaState();
                default:
                    throw new RegraNegocioException("Status de venda inválido.");
            }
        }
    }


}
