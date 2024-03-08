using ControleVenda.Domain;

namespace ControleVenda.Application.Abstactions.Queries
{
    public interface IVendedorQuery
    {
        Task<Vendedor?> SelecionarPorCpfAsync(string cpf);
    }
}
