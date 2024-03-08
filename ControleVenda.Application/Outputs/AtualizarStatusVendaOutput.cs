namespace ControleVenda.Application.Outputs
{
    public class AtualizarStatusVendaOutput(bool result, string? mensagem)
    {
        public bool Result { get;  } = result;
        public string? Mensagem { get; } = mensagem;
    }
}
