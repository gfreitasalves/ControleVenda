using ControleVenda.Application.Abstactions.Commands;
using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.UseCases;
using ControleVenda.Infrastructure.Database;
using ControleVenda.Infrastructure.Database.Queries;

namespace ControleVenda.Api
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Commands
            services.AddScoped<IVendaCommand, VendaCommand>();

            //Queries
            services.AddScoped<IItemVendaQuery, ItemVendaQuery>();
            services.AddScoped<IVendaQuery, VendaQuery>();
            services.AddScoped<IVendedorQuery, VendedorQuery>();

            //Use cases
            services.AddScoped<IAtualizarStatusVendaUseCase, AtualizarStatusVendaUseCase>();
            services.AddScoped<IBuscarVendaUseCase, BuscarVendaUseCase>();
            services.AddScoped<IRegistrarVendaUseCase, RegistrarVendaUseCase>();

            return services;
        }
    }
}
