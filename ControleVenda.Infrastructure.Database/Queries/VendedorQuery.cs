using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Domain;
using Microsoft.EntityFrameworkCore;

namespace ControleVenda.Infrastructure.Database.Queries
{
    public class VendedorQuery : IVendedorQuery
    {
        private readonly ControleVendaDbContext _controleVendaDbContext;

        public VendedorQuery(ControleVendaDbContext controleVendaDbContext)
        {
            _controleVendaDbContext = controleVendaDbContext;
        }

        public async Task<Vendedor?> SelecionarPorCpfAsync(string cpf)
        {
            //sempre retorna o primeiro por fins de teste
            return await _controleVendaDbContext.Vendedores.FirstOrDefaultAsync();
        }
    }
}
