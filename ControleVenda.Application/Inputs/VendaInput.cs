namespace ControleVenda.Application.Inputs
{
    public class VendaInput
    {
        public VendedorInput Vendedor { get; set; } = new();
        public IEnumerable<ItemVendaInput> Itens { get; set; } = [];
    }
}
