using ControleVenda.Application.Abstactions.Commands;
using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Inputs;
using ControleVenda.Application.UseCases;
using ControleVenda.Domain;
using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;
using ControleVenda.Domain.Venda;

namespace ControleVenda.Tests.Application
{
    [TestClass]
    public class RegistrarVendaUseCaseTests
    {
        [TestMethod]

        public async Task QuandoRegistrarVendaDeveRegistrar()
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>()
            {
                new ItemVenda("Item 001", 100),
                new ItemVenda("Item 011", 11)
            };

            var venda = new Venda(vendedor, itens, StatusVenda.AguardandoPagamento);

            var vendedorQueryMock = new Mock<IVendedorQuery>();
            var itemVendaQueryMock = new Mock<IItemVendaQuery>();
            var vendaCommandMock = new Mock<IVendaCommand>();

            vendedorQueryMock.Setup(x => x.SelecionarPorCpfAsync(It.IsAny<string>()))
                .ReturnsAsync(vendedor);
            itemVendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(itens.First());
            vendaCommandMock.Setup(x => x.InserirAsync(It.IsAny<Venda>()))
                .ReturnsAsync(venda);


            var registrarVendaUseCase = new RegistrarVendaUseCase(vendedorQueryMock.Object,
                                                                  itemVendaQueryMock.Object,
                                                                  vendaCommandMock.Object);

            //Act
            var vendaInput = new VendaInput()
            {
                Vendedor = new()
                {
                    CPF = vendedor.Cpf
                },
                Itens = itens.Select(i => new ItemVendaInput() { Id = i.Id })
            };

            var result = await registrarVendaUseCase.Registrar(vendaInput);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(venda.Id);
        }

        [TestMethod]
        public void QuandoRegistrarVendaSemItemsDeveLancarException()
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            
            var venda = new Venda(vendedor, [], StatusVenda.AguardandoPagamento);

            var vendedorQueryMock = new Mock<IVendedorQuery>();
            var itemVendaQueryMock = new Mock<IItemVendaQuery>();
            var vendaCommandMock = new Mock<IVendaCommand>();

            vendedorQueryMock.Setup(x => x.SelecionarPorCpfAsync(It.IsAny<string>()))
                .ReturnsAsync(vendedor);
            
            vendaCommandMock.Setup(x => x.InserirAsync(It.IsAny<Venda>()))
                .ReturnsAsync(venda);


            var registrarVendaUseCase = new RegistrarVendaUseCase(vendedorQueryMock.Object,
                                                                  itemVendaQueryMock.Object,
                                                                  vendaCommandMock.Object);

            //Act
            var vendaInput = new VendaInput()
            {
                Vendedor = new()
                {
                    CPF = vendedor.Cpf
                },
                Itens = []
            };

            Func<Task> action = async () => await registrarVendaUseCase.Registrar(vendaInput);

            //Assert
            action.Should().ThrowAsync<RegraNegocioException>()
                .WithMessage("Uma venda deve conter pelo menos um item.");
        }

        [TestMethod]
        public void QuandoRegistrarVendaComVendedorInvalidoLancarException()
        {
            //Arrange                                    
            var vendedorQueryMock = new Mock<IVendedorQuery>();
            var itemVendaQueryMock = new Mock<IItemVendaQuery>();
            var vendaCommandMock = new Mock<IVendaCommand>();

            vendedorQueryMock.Setup(x => x.SelecionarPorCpfAsync(It.IsAny<string>()))
                .ReturnsAsync(default(Vendedor?));


            var registrarVendaUseCase = new RegistrarVendaUseCase(vendedorQueryMock.Object,
                                                                  itemVendaQueryMock.Object,
                                                                  vendaCommandMock.Object);

            //Act
            var vendaInput = new VendaInput()
            {
                Vendedor = new()
                {
                    CPF = "112233445566"
                },
                Itens = []
            };

            Func<Task> action = async () => await registrarVendaUseCase.Registrar(vendaInput);

            //Assert
            action.Should().ThrowAsync<RegraNegocioException>()
                .WithMessage("Vendedor não encontrado.");

        }

        [TestMethod]
        public void QuandoRegistrarVendaComItemInvalidoDeveLancarException()
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>()
            {
                new ItemVenda("Item 001", 100)
            };

            var vendedorQueryMock = new Mock<IVendedorQuery>();
            var itemVendaQueryMock = new Mock<IItemVendaQuery>();
            var vendaCommandMock = new Mock<IVendaCommand>();

            vendedorQueryMock.Setup(x => x.SelecionarPorCpfAsync(It.IsAny<string>()))
                .ReturnsAsync(vendedor);
            itemVendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(ItemVenda?));

            var registrarVendaUseCase = new RegistrarVendaUseCase(vendedorQueryMock.Object,
                                                                  itemVendaQueryMock.Object,
                                                                  vendaCommandMock.Object);
            //Act
            var vendaInput = new VendaInput()
            {
                Vendedor = new()
                {
                    CPF = "112233445566"
                },
                Itens = itens.Select(i => new ItemVendaInput() { Id = i.Id })
            };
            
            Func<Task> action = async () => await registrarVendaUseCase.Registrar(vendaInput);

            //Assert
            action.Should().ThrowAsync<RegraNegocioException>()
                .WithMessage($"Item({itens.First().Id}) não encontrado.");

        }
    }
}