namespace Vendinha.Dtos
{
    public record ResultadoPaginado<T>(
        IReadOnlyList<T> Itens,
        int Pagina,
        int TamanhoPagina,
        int TotalItens,
        int TotalPaginas);
}
