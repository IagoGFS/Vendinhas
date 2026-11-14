using Vendinha.Models;

namespace Vendinha.Dtos
{
    public record ClienteResponse(
        int Id,
        string NomeCompleto,
        string Cpf,
        DateOnly DataNascimento,
        int Idade,
        string Email,
        decimal TotalDividas,
        decimal TotalEmAberto)
    {
        public static ClienteResponse De(Cliente c, decimal totalDividas, decimal totalEmAberto)
        {
            return new ClienteResponse(c.Id, c.NomeCompleto, c.Cpf, c.DataNascimento,
                c.Idade, c.Email, totalDividas, totalEmAberto);
        }

        // É usado quando as dívidas do cliente já foram carregadas (Include)
        public static ClienteResponse De(Cliente c)
        {
            var total = c.Dividas.Sum(d => d.Valor);
            var emAberto = c.Dividas.Where(d => !d.Paga).Sum(d => d.Valor);
            return De(c, total, emAberto);
        }
    }
}
