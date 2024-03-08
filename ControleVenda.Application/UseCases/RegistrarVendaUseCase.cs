using ControleVenda.Application.Abstactions.Commands;
using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.Inputs;
using ControleVenda.Application.Outputs;
using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;
using ControleVenda.Domain.Venda;

namespace ControleVenda.Application.UseCases
{
    public class RegistrarVendaUseCase(
                                        IVendedorQuery vendedorQuery,
                                        IItemVendaQuery itemVendaQuery,
                                        IVendaCommand vendaCommand
                                      )
        : IRegistrarVendaUseCase
    {
        private readonly IVendedorQuery _vendedorQuery = vendedorQuery;
        private readonly IItemVendaQuery _itemVendaQuery = itemVendaQuery;
        private readonly IVendaCommand _vendaCommand = vendaCommand;

        public async Task<VendaOutput> Registrar(VendaInput vendaInput)
        {
            var vendedor = await _vendedorQuery.SelecionarPorCpfAsync(vendaInput.Vendedor.CPF)
                                    ?? throw new RegraNegocioException("Vendedor não encontrado.");

            var itensVenda = CarregarItens(vendaInput.Itens);

            var venda = new Venda(vendedor, itensVenda, StatusVenda.AguardandoPagamento);

            var result = await _vendaCommand.InserirAsync(venda);

            return new VendaOutput(
                                    result.Id,
                                    result.Vendedor.Nome,
                                    result.Vendedor.Cpf,
                                    result.Data,
                                    result.Status.ToString(),
                                    result.Itens.Select(i => new ItemVendaOutput(i.Id, i.Nome, i.Valor))
                                    );
        }

        private List<ItemVenda> CarregarItens(IEnumerable<ItemVendaInput> inputItens)
        {
            if (!inputItens.Any()) throw new RegraNegocioException("Uma venda deve conter pelo menos um item.");

            var itens = inputItens.Select(async i => await _itemVendaQuery.SelecionarPorIdAsync(i.Id)
                                                                   ?? throw new RegraNegocioException($"Item({i.Id}) não encontrado."))
                                    .Select(i => i.Result).ToList();

            return itens;
        }
    }
}
