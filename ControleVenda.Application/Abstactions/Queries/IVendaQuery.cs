using ControleVenda.Domain.Venda;

namespace ControleVenda.Application.Abstactions.Queries
{
    public interface IVendaQuery
    {
        Task<Venda?> SelecionarPorIdAsync(Guid id);
    }
}
