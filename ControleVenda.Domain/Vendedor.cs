namespace ControleVenda.Domain
{
    public class Vendedor
    {
        public Guid Id { get; }
        public string Cpf { get; }
        public string Nome { get; }
        public string Email { get; }
        public string Telefone { get; }

        public Vendedor(string cpf, string nome, string email, string telefone) :
            this(Guid.NewGuid(), cpf, nome, email, telefone)
        {
        }

        private Vendedor(Guid id, string cpf, string nome, string email, string telefone)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio.", nameof(cpf));
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser nulo ou vazio.", nameof(nome));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser nulo ou vazio.", nameof(email));
            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("Telefone não pode ser nulo ou vazio.", nameof(telefone));

            Id = id;
            Cpf = cpf;
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }
    }
}
