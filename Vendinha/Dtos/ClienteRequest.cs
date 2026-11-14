namespace Vendinha.Dtos
{
    /// Corpo aceito no POST e no PUT de cliente.
    public record ClienteRequest(
        string NomeCompleto,
        string Cpf,
        DateOnly? DataNascimento,
        string Email);
}
