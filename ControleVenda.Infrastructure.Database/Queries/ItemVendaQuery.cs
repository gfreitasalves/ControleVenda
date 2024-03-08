using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Domain.Venda;
using Microsoft.EntityFrameworkCore;

namespace ControleVenda.Infrastructure.Database.Queries
{
    public class ItemVendaQuery : IItemVendaQuery
    {
        private readonly ControleVendaDbContext _controleVendaDbContext;

        public ItemVendaQuery(ControleVendaDbContext controleVendaDbContext)
        {
            _controleVendaDbContext = controleVendaDbContext;
        }

        public async Task<ItemVenda?> SelecionarPorIdAsync(Guid id)
        {
            //sempre retorna o primeiro por fins de teste
            return await _controleVendaDbContext.ItensVenda.FirstOrDefaultAsync();
        }
    }
}
