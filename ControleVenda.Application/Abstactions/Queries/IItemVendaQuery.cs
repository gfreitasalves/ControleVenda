using ControleVenda.Domain.Venda;

namespace ControleVenda.Application.Abstactions.Queries
{
    public interface IItemVendaQuery
    {
        Task<ItemVenda?> SelecionarPorIdAsync(Guid id);
    }
}
