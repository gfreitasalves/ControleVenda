using ControleVenda.Application.Abstactions.Commands;
using ControleVenda.Domain.Venda;

namespace ControleVenda.Infrastructure.Database
{
    public class VendaCommand : IVendaCommand
    {
        private readonly ControleVendaDbContext _controleVendaDbContext;

        public VendaCommand(ControleVendaDbContext controleVendaDbContext)
        {
            _controleVendaDbContext = controleVendaDbContext;
        }

        public async Task<Venda> InserirAsync(Venda venda)
        {
            _controleVendaDbContext.Vendas.Add(venda);

            _ = await _controleVendaDbContext.SaveChangesAsync();

            return venda;
        }

        public async Task<Venda> AtualizarAsync(Venda venda)
        {
            _controleVendaDbContext.Vendas.Update(venda);

            _ = await _controleVendaDbContext.SaveChangesAsync();

            return venda;
        }
    }
}
