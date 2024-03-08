using ControleVenda.Application.Outputs;

namespace ControleVenda.Application.Abstactions.UseCases
{
    public interface IBuscarVendaUseCase
    {
        Task<VendaOutput?> Buscar(Guid id);
    }
}
