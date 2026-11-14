using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "O valor da dívida deve ser maior que zero.")]
        public decimal Valor { get; set; }

        // Paga (true) ou em aberto (false)
        public bool Paga { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataPagamento { get; set; }

        public Cliente Cliente { get; set; }
    }
}
