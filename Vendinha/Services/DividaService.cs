using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class DividaService
    {
        private readonly VendinhaDbContext context;

        public DividaService(VendinhaDbContext context)
        {
            this.context = context;
        }

        public DividaService() : this(new VendinhaDbContext(VendinhaDbContext.ConnectionStringPadrao))
        {
        }

        public List<Divida> Listar(int clienteId)
        {
            return context.Dividas
                .AsNoTracking()
                .Where(d => d.ClienteId == clienteId)
                .OrderByDescending(d => d.DataCriacao)
                .ToList();
        }

        public Divida Obter(int clienteId, int dividaId)
        {
            return context.Dividas
                .FirstOrDefault(d => d.Id == dividaId && d.ClienteId == clienteId);
        }

        public bool Criar(int clienteId, Divida divida, out List<ValidationResult> erros)
        {
            // Situação e datas não vêm do cliente da API: são definidas aqui
            divida.ClienteId = clienteId;
            divida.Valor = Math.Round(divida.Valor, 2);
            divida.Paga = false;
            divida.DataCriacao = DateTime.UtcNow;
            divida.DataPagamento = null;

            var vcontext = new ValidationContext(divida);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(divida, vcontext, erros, true);

            // Regra: um cliente só pode ter uma dívida em aberto
            var temEmAberto = context.Dividas.Any(d => d.ClienteId == clienteId && !d.Paga);
            if (temEmAberto)
            {
                erros.Add(new ValidationResult(
                    "O cliente já possui uma dívida em aberto.", ["ClienteId"]));
            }

            if (erros.Count > 0)
            {
                return false;
            }

            context.Add(divida);
            context.SaveChanges();
            return true;
        }

        public bool Pagar(Divida divida, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();

            if (divida.Paga)
            {
                erros.Add(new ValidationResult("A dívida já foi paga.", ["Paga"]));
                return false;
            }

            divida.Paga = true;
            divida.DataPagamento = DateTime.UtcNow;
            context.SaveChanges();
            return true;
        }
    }
}
