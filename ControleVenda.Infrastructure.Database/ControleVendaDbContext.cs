using ControleVenda.Domain;
using ControleVenda.Domain.Venda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace ControleVenda.Infrastructure.Database
{
    public class ControleVendaDbContext : DbContext
    {
        public ControleVendaDbContext(DbContextOptions<ControleVendaDbContext> options)
          : base(options)
        {
            AdicionarDadosDeTeste();
        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#pragma warning disable CS8603 // Possible null reference return.
            var vendedorConverter = new ValueConverter<Vendedor, string>(
                                                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                                                v => JsonSerializer.Deserialize<Vendedor>(v, JsonSerializerOptions.Default));
#pragma warning restore CS8603 // Possible null reference return.

#pragma warning disable CS8603 // Possible null reference return.
            var itensConverter = new ValueConverter<List<ItemVenda>, string>(
                                                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                                                v => JsonSerializer.Deserialize<List<ItemVenda>>(v, JsonSerializerOptions.Default));
#pragma warning restore CS8603 // Possible null reference return.


            modelBuilder.Entity<Vendedor>(x =>
            {
                x.HasKey(l => l.Id);
                x.Property(e => e.Id);
                x.Property(e => e.Cpf);
                x.Property(e => e.Nome);
                x.Property(e => e.Email);
                x.Property(e => e.Telefone);
            });

            modelBuilder.Entity<Venda>(x =>
            {
                x.HasKey(l => l.Id);
                x.Property(e => e.Id);
                x.Property(e => e.Vendedor).HasConversion(vendedorConverter);
                x.Property(e => e.Itens).HasConversion(itensConverter);
                x.Property(e => e.Status);
                x.Property(e => e.Data);
            });

            modelBuilder.Entity<ItemVenda>(x =>
            {
                x.HasKey(l => l.Id);
                x.Property(e => e.Id);
                x.Property(e => e.Nome);
                x.Property(e => e.Valor);
            });

            base.OnModelCreating(modelBuilder);
        }

        public void AdicionarDadosDeTeste()
        {
            if (!Vendedores.Any())
            {
                Vendedores.Add(new Vendedor("11111111111", "Claudio Vendedor", "claudio@teste.com", "22222222"));

                ItensVenda.Add(new ItemVenda("Casaco", 100));
                ItensVenda.Add(new ItemVenda("Camisa T Branca", 100));
                ItensVenda.Add(new ItemVenda("Meia", 100));
                ItensVenda.Add(new ItemVenda("Camisa Polo", 100));

                _ = SaveChanges();
            }
        }
    }

}
