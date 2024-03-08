using ControleVenda.Application.Inputs;
using ControleVenda.Application.Outputs;

namespace ControleVenda.Application.Abstactions.UseCases
{
    public interface IAtualizarStatusVendaUseCase
    {
        Task<AtualizarStatusVendaOutput> Atualizar(AtualizarStatusInput atualizarStatusInput);
    }
}
