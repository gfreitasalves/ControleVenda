using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Domain.Venda;
using Microsoft.EntityFrameworkCore;

namespace ControleVenda.Infrastructure.Database.Queries
{
    public class VendaQuery : IVendaQuery
    {
        private readonly ControleVendaDbContext _controleVendaDbContext;

        public VendaQuery(ControleVendaDbContext controleVendaDbContext)
        {
            _controleVendaDbContext = controleVendaDbContext;
        }

        public async Task<Venda?> SelecionarPorIdAsync(Guid id)
        {
            return await _controleVendaDbContext.Vendas.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
