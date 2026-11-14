namespace Vendinha.Dtos
{
    /// Corpo aceito no POST de dívida. Situação e datas são definidas pelo sistema.
    public record DividaRequest(decimal? Valor);
}
