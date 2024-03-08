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
    public class AtualizarStatusVendaUseCaseTests
    {
        [DataTestMethod]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.PagamentoAprovado)]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.Cancelada)]
        [DataRow(StatusVenda.PagamentoAprovado, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.PagamentoAprovado, StatusVenda.Cancelada)]
        [DataRow(StatusVenda.EnviadoParaTransportadora, StatusVenda.Entregue)]
        public async Task QuandoMudarEstadoDeInicialValidoParaFinalValidoDeveMudar(StatusVenda statusInicial, StatusVenda statusFinal)
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>();
            var venda = new Venda(vendedor, itens, statusInicial);

            var vendaQueryMock = new Mock<IVendaQuery>();
            var vendaCommandMock = new Mock<IVendaCommand>();

            vendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(venda);

            vendaCommandMock.Setup(x => x.AtualizarAsync(It.IsAny<Venda>()))
                .ReturnsAsync(venda);

            var atualizarStatusVendaUseCase = new AtualizarStatusVendaUseCase(vendaQueryMock.Object, vendaCommandMock.Object);

            //Act

            var atualizarStatusInput = new AtualizarStatusInput()
            {
                IdVenda = Guid.NewGuid(),
                Status = statusFinal.ToString(),
            };

            var result = await atualizarStatusVendaUseCase.Atualizar(atualizarStatusInput);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeTrue();
            result.Mensagem.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.Entregue)]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.Entregue, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.Cancelada, StatusVenda.EnviadoParaTransportadora)]
        public async Task QuandoMudarEstadoDeInicialValidoParaFinalInvalidoDeveLancarException(StatusVenda statusInicial, StatusVenda statusFinal)
        {
            //Arrange            
            var mensagem = $"Transição de status inválida de:'{statusInicial}' para:'{statusFinal}'.";
                        
            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>();
            var venda = new Venda(vendedor, itens, statusInicial);

            var vendaQueryMock = new Mock<IVendaQuery>();

            var vendaCommandMock = new Mock<IVendaCommand>();

            vendaQueryMock.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(venda);

            var atualizarStatusVendaUseCase = new AtualizarStatusVendaUseCase(vendaQueryMock.Object, vendaCommandMock.Object);

            //Act
            var atualizarStatusInput = new AtualizarStatusInput()
            {
                IdVenda = Guid.NewGuid(),
                Status = statusFinal.ToString()
            };

            var result = await atualizarStatusVendaUseCase.Atualizar(atualizarStatusInput);

            //Assert
            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeFalse();
            result.Mensagem.Should().NotBeNull();
            result.Mensagem.Should().Be(mensagem);
        }


    }
}