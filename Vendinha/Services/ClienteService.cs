using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Dtos;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class ClienteService
    {
        public const int TamanhoPagina = 10;

        private readonly VendinhaDbContext context;

        public ClienteService(VendinhaDbContext context)
        {
            this.context = context;
        }

        public ClienteService() : this(new VendinhaDbContext(VendinhaDbContext.ConnectionStringPadrao))
        {
        }


        /// Lista clientes de 10 em 10, de quem mais deve para quem menos deve,
        /// com filtro opcional por texto contido no nome.
        public ResultadoPaginado<ClienteResponse> Listar(string pesquisa, int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            var query = context.Clientes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                var termo = pesquisa.Trim().ToLower();
                query = query.Where(c => c.NomeCompleto.ToLower().Contains(termo));
            }

            var totalItens = query.Count();
            var totalPaginas = (int)Math.Ceiling(totalItens / (double)TamanhoPagina);

            // as somas são feitas no banco; a idade é calculada na resposta
            var registros = query
                .Select(c => new
                {
                    Cliente = c,
                    TotalDividas = c.Dividas.Sum(d => d.Valor),
                    TotalEmAberto = c.Dividas.Where(d => !d.Paga).Sum(d => d.Valor)
                })
                .OrderByDescending(x => x.TotalEmAberto)
                .ThenBy(x => x.Cliente.NomeCompleto)
                .ThenBy(x => x.Cliente.Id)
                .Skip((page - 1) * TamanhoPagina)
                .Take(TamanhoPagina)
                .ToList();

            var itens = registros
                .Select(x => ClienteResponse.De(x.Cliente, x.TotalDividas, x.TotalEmAberto))
                .ToList();

            return new ResultadoPaginado<ClienteResponse>(
                itens, page, TamanhoPagina, totalItens, totalPaginas);
        }

        public Cliente Obter(int id)
        {
            return context.Clientes
                .Include(c => c.Dividas)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool Existe(int id)
        {
            return context.Clientes.Any(c => c.Id == id);
        }

        public bool Criar(Cliente cliente, out List<ValidationResult> erros)
        {
            if (!Validar(cliente, out erros))
            {
                return false;
            }

            context.Add(cliente);  // INSERT
            context.SaveChanges(); // Commit
            return true;
        }

        public bool Atualizar(int id, Cliente dados, out List<ValidationResult> erros)
        {
            var cliente = context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente is null)
            {
                erros = new List<ValidationResult>
                {
                    new ValidationResult("Cliente não encontrado.", ["Id"])
                };
                return false;
            }

            dados.Id = id;
            if (!Validar(dados, out erros))
            {
                return false;
            }

            cliente.NomeCompleto = dados.NomeCompleto;
            cliente.Cpf = dados.Cpf;
            cliente.DataNascimento = dados.DataNascimento;
            cliente.Email = dados.Email;

            context.SaveChanges(); // UPDATE
            return true;
        }

        public bool Excluir(int id)
        {
            var cliente = context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente is null)
            {
                return false;
            }

            // As dívidas do cliente são apagadas junto (ON DELETE CASCADE)
            context.Remove(cliente);
            context.SaveChanges();
            return true;
        }

        public bool Validar(Cliente cliente, out List<ValidationResult> erros)
        {
            var vcontext = new ValidationContext(cliente);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(cliente, vcontext, erros, true);

            var hoje = DateOnly.FromDateTime(DateTime.Today);
            if (cliente.DataNascimento == default)
            {
                erros.Add(new ValidationResult(
                    "A data de nascimento é obrigatória.", ["DataNascimento"]));
            }
            else if (cliente.DataNascimento > hoje)
            {
                erros.Add(new ValidationResult(
                    "A data de nascimento não pode estar no futuro.", ["DataNascimento"]));
            }

            // Apenas um cliente por CPF (ignora o próprio cliente na atualização)
            var duplicado = !string.IsNullOrEmpty(cliente.Cpf) &&
                context.Clientes.Any(c => c.Cpf == cliente.Cpf && c.Id != cliente.Id);
            if (duplicado)
            {
                erros.Add(new ValidationResult(
                    $"Já existe um cliente cadastrado com o CPF {cliente.Cpf}.", ["Cpf"]));
            }

            return erros.Count == 0;
        }
    }
}
