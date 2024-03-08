namespace ControleVenda.Application.Inputs
{
    public  class AtualizarStatusInput
    {
        public Guid IdVenda { get; set; }
        public string Status { get; set; }=string.Empty;
    }
}
