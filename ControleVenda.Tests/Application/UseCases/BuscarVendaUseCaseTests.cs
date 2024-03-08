using ControleVenda.Application.Abstactions.Queries;
using ControleVenda.Application.Abstactions.UseCases;
using ControleVenda.Application.UseCases;
using ControleVenda.Domain;
using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;
using ControleVenda.Domain.Venda;

namespace ControleVenda.Tests.Application
{
    [TestClass]
    public class BuscarVendaUseCaseTests
    {
        [TestMethod]
        
        public async Task QuandoBuscarVendaDeveRetornar()
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>();
            var venda = new Venda(vendedor, itens, StatusVenda.AguardandoPagamento);

            var vendaQueryMock = new Mock<IVendaQuery>();

            vendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(venda);
             
            var atualizarStatusVendaUseCase = new BuscarVendaUseCase(vendaQueryMock.Object);

            //Act
            var result = await atualizarStatusVendaUseCase.Buscar(Guid.NewGuid());

            //Assert
            result.Should().NotBeNull();                        
        }

        [TestMethod]

        public async Task QuandoBuscarVendaDeveRetornarNull()
        {
            //Arrange                        
            var vendaQueryMock = new Mock<IVendaQuery>();

            vendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(Venda?));

            var atualizarStatusVendaUseCase = new BuscarVendaUseCase(vendaQueryMock.Object);

            //Act
            var result = await atualizarStatusVendaUseCase.Buscar(Guid.NewGuid());

            //Assert
            result.Should().BeNull();
        }
    }
}