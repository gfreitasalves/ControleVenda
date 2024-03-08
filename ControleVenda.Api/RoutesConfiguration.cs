using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.Inputs;

namespace ControleVenda.Api
{
    public static class RoutesConfiguration
    {
        public static WebApplication RegisterRoutes(this WebApplication app)
        {

            //AtualizarStatusVendaUseCase
            app.MapPut("/Venda/Status", async (AtualizarStatusInput atualizarStatusInput, IAtualizarStatusVendaUseCase atualizarStatusVendaUseCase) =>
             await atualizarStatusVendaUseCase.Atualizar(atualizarStatusInput))
            .WithName("AtualizarStatusVenda")
            .WithOpenApi();

            //BuscarVendaUseCase
            app.MapGet("/Venda/{id}", async (Guid id, IBuscarVendaUseCase buscarVendaUseCase) =>
                await buscarVendaUseCase.Buscar(id))
           .WithName("BuscarVenda")
           .WithOpenApi();

            //RegistrarVendaUseCase
            app.MapPost("/Venda", async (VendaInput vendaInput, IRegistrarVendaUseCase registrarVendaUseCase) =>
                 await registrarVendaUseCase.Registrar(vendaInput))
           .WithName("RegistrarVenda")
           .WithOpenApi();

            return app;
        }
    }
}
