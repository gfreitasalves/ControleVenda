using ControleVenda.Domain.Venda;

namespace ControleVenda.Application.Abstactions.Commands
{
    public interface IVendaCommand
    {
        Task<Venda> InserirAsync(Venda venda);
        Task<Venda> AtualizarAsync(Venda venda);
    }
}
