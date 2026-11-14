using Vendinha.Models;

namespace Vendinha.Dtos
{
    public record DividaResponse(
        int Id,
        int ClienteId,
        decimal Valor,
        bool Paga,
        string Situacao,
        DateTime DataCriacao,
        DateTime? DataPagamento)
    {
        public static DividaResponse De(Divida d)
        {
            return new DividaResponse(d.Id, d.ClienteId, d.Valor, d.Paga,
                d.Paga ? "Paga" : "Em aberto", d.DataCriacao, d.DataPagamento);
        }
    }
}
