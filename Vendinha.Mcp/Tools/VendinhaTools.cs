using System.ComponentModel;
using ModelContextProtocol.Server;
using Vendinha.Dtos;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Mcp.Tools
{
    [McpServerToolType]
    public class VendinhaTools
    {
        private readonly ClienteService clienteService;
        private readonly DividaService dividaService;

        public VendinhaTools(ClienteService clienteService, DividaService dividaService)
        {
            this.clienteService = clienteService;
            this.dividaService = dividaService;
        }

        [McpServerTool]
        [Description("Lista os clientes da vendinha de 10 em 10, de quem mais deve para quem menos deve. " +
            "Use 'pesquisa' para filtrar por parte do nome (vazio lista todos).")]
        public ResultadoPaginado<ClienteResponse> ConsultarClientes(string pesquisa, int page)
        {
            return clienteService.Listar(pesquisa, page);
        }

        [McpServerTool]
        [Description("Cadastra um novo cliente. O CPF deve ser válido; o e-mail é opcional (envie vazio se não houver).")]
        public object CriarCliente(string nomeCompleto, string cpf, DateTime dataNascimento, string email)
        {
            var cliente = new Cliente
            {
                NomeCompleto = nomeCompleto,
                Cpf = cpf,
                DataNascimento = DateOnly.FromDateTime(dataNascimento),
                Email = email
            };

            var sucesso = clienteService.Criar(cliente, out var erros);
            return sucesso ? ClienteResponse.De(cliente) : ErroResponse.De(erros);
        }

        [McpServerTool]
        [Description("Lista as dívidas (penduradas) de um cliente, da mais recente para a mais antiga.")]
        public object ConsultarDividas(int clienteId)
        {
            if (!clienteService.Existe(clienteId))
            {
                return "Cliente não encontrado";
            }

            return dividaService.Listar(clienteId).Select(DividaResponse.De).ToList();
        }

        [McpServerTool]
        [Description("Pendura uma nova dívida para o cliente. Cada cliente só pode ter uma dívida em aberto.")]
        public object PendurarDivida(int clienteId, decimal valor)
        {
            if (!clienteService.Existe(clienteId))
            {
                return "Cliente não encontrado";
            }

            var divida = new Divida { Valor = valor };
            var sucesso = dividaService.Criar(clienteId, divida, out var erros);
            return sucesso ? DividaResponse.De(divida) : ErroResponse.De(erros);
        }

        [McpServerTool]
        [Description("Marca uma dívida do cliente como paga.")]
        public object PagarDivida(int clienteId, int dividaId)
        {
            var divida = dividaService.Obter(clienteId, dividaId);
            if (divida is null)
            {
                return "Dívida não encontrada";
            }

            var sucesso = dividaService.Pagar(divida, out var erros);
            return sucesso ? DividaResponse.De(divida) : ErroResponse.De(erros);
        }
    }
}
