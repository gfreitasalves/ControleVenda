using ControleVenda.Application.Inputs;
using ControleVenda.Application.Outputs;

namespace ControleVenda.Application.Abstactions.UseCases
{
    public interface IRegistrarVendaUseCase
    {
        Task<VendaOutput> Registrar(VendaInput venda);
    }
}
