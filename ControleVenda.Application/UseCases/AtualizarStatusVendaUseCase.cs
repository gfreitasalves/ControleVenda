using ControleVenda.Application.Abstactions.Commands;
using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.Inputs;
using ControleVenda.Application.Outputs;
using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;

namespace ControleVenda.Application.UseCases
{
    public class AtualizarStatusVendaUseCase(IVendaQuery vendaQuery,
                                                IVendaCommand vendaCommand)
        : IAtualizarStatusVendaUseCase
    {
        private readonly IVendaQuery _vendaQuery = vendaQuery;

        private readonly IVendaCommand _vendaCommand = vendaCommand;

        public async Task<AtualizarStatusVendaOutput> Atualizar(AtualizarStatusInput atualizarStatusInput)
        {
            var result = true;
            var mensagem = default(string?);

            try
            {
                var venda = await _vendaQuery.SelecionarPorIdAsync(atualizarStatusInput.IdVenda)
                                    ?? throw new RegraNegocioException("Venda não encontrada.");

                if (Enum.TryParse(atualizarStatusInput.Status, out StatusVenda status))
                {
                    venda.AtualizarStatus(status);

                    _ = _vendaCommand.AtualizarAsync(venda);
                }
                else 
                {
                    throw new RegraNegocioException("Status inválido.");
                }
            }            
            catch (RegraNegocioException ex)
            {
                result = false;
                mensagem = ex.Message;
            }            
            catch
            {
                //TODO: log exception

                result = false;
                mensagem = "Ocorreu um erro ao atualizar o status.";
            }

            return new AtualizarStatusVendaOutput(result, mensagem);
        }
    }
}
