using System.ComponentModel.DataAnnotations;

namespace Vendinha.Validations
{

    /// Valida um CPF pelos dígitos verificadores.
    /// Aceita com ou sem máscara (123.456.789-09 ou 12345678909).
    /// Valor vazio é ignorado: quem exige o preenchimento é o [Required].
    public class CpfAttribute : ValidationAttribute
    {
        public CpfAttribute() : base("O CPF informado é inválido.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not string texto)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                return true;
            }

            return CpfEhValido(texto);
        }

        public static bool CpfEhValido(string cpf)
        {
            var digitos = cpf.Trim().Replace(".", "").Replace("-", "");

            if (digitos.Length != 11 || !digitos.All(char.IsDigit))
            {
                return false;
            }

            // 000.000.000-00, 111.111.111-11 etc. passam na conta, mas são inválidos
            if (digitos.All(c => c == digitos[0]))
            {
                return false;
            }

            var primeiro = CalcularDigito(digitos, 9);
            var segundo = CalcularDigito(digitos, 10);

            return digitos[9] - '0' == primeiro && digitos[10] - '0' == segundo;
        }

        private static int CalcularDigito(string digitos, int quantidade)
        {
            var soma = 0;
            for (int i = 0; i < quantidade; i++)
            {
                soma += (digitos[i] - '0') * (quantidade + 1 - i);
            }

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
