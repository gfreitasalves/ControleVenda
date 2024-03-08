using ControleVenda.Domain;
using ControleVenda.Domain.Enums;
using ControleVenda.Domain.Exceptions;
using ControleVenda.Domain.Venda;

namespace ControleVenda.Tests.Domain
{
    [TestClass]
    public class VendaTests
    {
        [DataTestMethod]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.PagamentoAprovado)]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.Cancelada)]
        [DataRow(StatusVenda.PagamentoAprovado, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.PagamentoAprovado, StatusVenda.Cancelada)]
        [DataRow(StatusVenda.EnviadoParaTransportadora, StatusVenda.Entregue)]
        public void QuandoMudarEstadoDeInicialValidoParaFinalValidoDeveMudar(StatusVenda statausInicial, StatusVenda statausFinal)
        {
            //Arrange            
            var vendedor = new Vendedor("112233445566","Teste","teste@teste.com","112222111");
            var itens = new List<ItemVenda>();

            var venda = new Venda(vendedor, itens, statausInicial);

            //Act
            venda.AtualizarStatus(statausFinal);

            //Assert
            venda.Should().NotBeNull();
            venda.Status.Should().Be(statausFinal);
        }

        [DataTestMethod]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.Entregue)]
        [DataRow(StatusVenda.AguardandoPagamento, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.Entregue, StatusVenda.EnviadoParaTransportadora)]
        [DataRow(StatusVenda.Cancelada, StatusVenda.EnviadoParaTransportadora)]
        public void QuandoMudarEstadoDeInicialValidoParaFinalInvalidoDeveLancarException(StatusVenda statausInicial, StatusVenda statausFinal)
        {
            //Arrange            
            var mensagem = $"Transição de status inválida de:'{statausInicial}' para:'{statausFinal}'.";

            var vendedor = new Vendedor("112233445566", "Teste", "teste@teste.com", "112222111");
            var itens = new List<ItemVenda>();

            var venda = new Venda(vendedor, itens, statausInicial);

            //Act
            Action action = () => venda.AtualizarStatus(statausFinal);

            //Assert
            action.Should().Throw<RegraNegocioException>().WithMessage(mensagem);
        }


    }
}