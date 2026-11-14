using System.ComponentModel.DataAnnotations;
using Vendinha.Validations;

namespace Vendinha.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        private string nomeCompleto = string.Empty;
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "O nome completo deve ter entre 3 e 150 caracteres.")]
        public string NomeCompleto
        {
            get { return this.nomeCompleto; }
            set { this.nomeCompleto = (value ?? string.Empty).Trim(); }
        }

        // Sempre guardado só com os 11 dígitos (sem pontos e traço)
        private string cpf = string.Empty;
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Cpf]
        public string Cpf
        {
            get { return this.cpf; }
            set
            {
                this.cpf = (value ?? string.Empty)
                    .Trim().Replace(".", "").Replace("-", "");
            }
        }

        // Obrigatório e não pode ser futura: validado no ClienteService.Validar
        public DateOnly DataNascimento { get; set; }

        private string email;
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
        public string Email
        {
            get { return this.email; }
            set
            {
                // opcional: vazio vira null, se informado, é validado pelo [EmailAddress]
                this.email = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim().ToLower();
            }
        }

        // Calculada, não existe coluna no banco
        public int Idade
        {
            get
            {
                var hoje = DateOnly.FromDateTime(DateTime.Today);
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento > hoje.AddYears(-idade))
                {
                    idade--;
                }
                return idade;
            }
        }

        public List<Divida> Dividas { get; set; } = new List<Divida>();
    }
}
