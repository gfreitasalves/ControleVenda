using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.Outputs;

namespace ControleVenda.Application.UseCases
{
    public class BuscarVendaUseCase(IVendaQuery vendaQuery)
        : IBuscarVendaUseCase
    {
        private readonly IVendaQuery _vendaQuery = vendaQuery;

        public async Task<VendaOutput?> Buscar(Guid id)
        {
            var venda = await _vendaQuery.SelecionarPorIdAsync(id);

            return venda != null ? new VendaOutput(
                                    venda.Id,
                                    venda.Vendedor.Nome,
                                    venda.Vendedor.Cpf,
                                    venda.Data,
                                    venda.Status.ToString(),
                                    venda.Itens.Select(i => new ItemVendaOutput(i.Id, i.Nome, i.Valor))
                                    ): null;
        }
    }
}
