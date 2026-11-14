using System.ComponentModel.DataAnnotations;

namespace Vendinha.Dtos
{
    public record ErroResponse(string Campo, string Mensagem)
    {
        public static List<ErroResponse> De(IEnumerable<ValidationResult> erros)
        {
            return erros
                .Select(e => new ErroResponse(e.MemberNames.FirstOrDefault(), e.ErrorMessage))
                .ToList();
        }
    }
}
