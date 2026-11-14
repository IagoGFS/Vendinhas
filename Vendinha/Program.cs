using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

// Versão console da Vendinha (mesmo padrão do Program.cs da Escola)
var cultura = new CultureInfo("pt-BR");
var clienteService = new ClienteService();
var dividaService = new DividaService();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== VENDINHA ==========");
    Console.WriteLine("1 - Listar clientes");
    Console.WriteLine("2 - Cadastrar cliente");
    Console.WriteLine("3 - Atualizar cliente");
    Console.WriteLine("4 - Excluir cliente");
    Console.WriteLine("5 - Listar dívidas de um cliente");
    Console.WriteLine("6 - Pendurar dívida");
    Console.WriteLine("7 - Marcar dívida como paga");
    Console.WriteLine("0 - Sair");

    Console.Write("Digite a opcao: ");
    if (!int.TryParse(Console.ReadLine(), out var opcao))
    {
        Console.WriteLine("Opção inválida!");
        continue;
    }

    try
    {
        if (opcao == 0)
        {
            break;
        }
        else if (opcao == 1)
        {
            ListarClientes();
        }
        else if (opcao == 2)
        {
            CadastrarCliente();
        }
        else if (opcao == 3)
        {
            AtualizarCliente();
        }
        else if (opcao == 4)
        {
            ExcluirCliente();
        }
        else if (opcao == 5)
        {
            ListarDividas();
        }
        else if (opcao == 6)
        {
            PendurarDivida();
        }
        else if (opcao == 7)
        {
            PagarDivida();
        }
        else
        {
            Console.WriteLine("Opção inválida!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro inesperado (o banco está acessível?): {0}", ex.Message);
    }
}

void ListarClientes()
{
    Console.Write("Pesquisar por nome (Enter para todos): ");
    var pesquisa = Console.ReadLine();
    Console.Write("Página (Enter para 1): ");
    int.TryParse(Console.ReadLine(), out var page);
    if (page < 1) page = 1;

    var resultado = clienteService.Listar(pesquisa, page);

    Console.WriteLine("==========================");
    foreach (var c in resultado.Itens)
    {
        Console.WriteLine("[{0}] {1} | CPF {2} | {3} anos | Deve: {4} | Total de dívidas: {5}",
            c.Id, c.NomeCompleto, c.Cpf, c.Idade,
            c.TotalEmAberto.ToString("C", cultura),
            c.TotalDividas.ToString("C", cultura));
    }
    Console.WriteLine("==========================");
    Console.WriteLine("Página {0} de {1} ({2} clientes)",
        resultado.Pagina, resultado.TotalPaginas, resultado.TotalItens);
}

Cliente LerCliente()
{
    Console.Write("Nome completo: ");
    var nome = Console.ReadLine();
    Console.Write("CPF: ");
    var cpf = Console.ReadLine();
    Console.Write("Data de nascimento (dd/mm/aaaa): ");
    DateOnly.TryParse(Console.ReadLine(), cultura, DateTimeStyles.None, out var data);
    Console.Write("E-mail (opcional): ");
    var email = Console.ReadLine();

    return new Cliente
    {
        NomeCompleto = nome ?? string.Empty,
        Cpf = cpf ?? string.Empty,
        DataNascimento = data, // inválida vira 01/01/0001 e a validação recusa
        Email = email
    };
}

void CadastrarCliente()
{
    var cliente = LerCliente();

    var sucesso = clienteService.Criar(cliente, out var erros);
    if (sucesso)
    {
        Console.WriteLine("Cliente cadastrado com sucesso! Id: {0}", cliente.Id);
    }
    else
    {
        Console.WriteLine("Falha no cadastro do cliente");
        foreach (var erro in erros)
        {
            Console.WriteLine("{0}: {1}", erro.MemberNames.FirstOrDefault(), erro.ErrorMessage);
        }
    }
}

void AtualizarCliente()
{
    var id = LerId("Id do cliente: ");
    if (id is null || !clienteService.Existe(id.Value))
    {
        Console.WriteLine("Cliente não encontrado.");
        return;
    }

    var dados = LerCliente();
    var sucesso = clienteService.Atualizar(id.Value, dados, out var erros);
    if (sucesso)
    {
        Console.WriteLine("Cliente atualizado com sucesso!");
    }
    else
    {
        Console.WriteLine("Falha na atualização do cliente");
        foreach (var erro in erros)
        {
            Console.WriteLine("{0}: {1}", erro.MemberNames.FirstOrDefault(), erro.ErrorMessage);
        }
    }
}

void ExcluirCliente()
{
    var id = LerId("Id do cliente: ");
    if (id is null)
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    Console.Write("Isso exclui também as dívidas do cliente. Confirma (s/n)? ");
    if (Console.ReadLine()?.Trim().ToLower() != "s")
    {
        Console.WriteLine("Cancelado.");
        return;
    }

    Console.WriteLine(clienteService.Excluir(id.Value)
        ? "Cliente excluído."
        : "Cliente não encontrado.");
}

void ListarDividas()
{
    var id = LerId("Id do cliente: ");
    if (id is null || !clienteService.Existe(id.Value))
    {
        Console.WriteLine("Cliente não encontrado.");
        return;
    }

    var dividas = dividaService.Listar(id.Value);
    Console.WriteLine("==========================");
    foreach (var d in dividas)
    {
        var situacao = "Em aberto";
        if (d.Paga && d.DataPagamento.HasValue)
        {
            situacao = "Paga em " + d.DataPagamento.Value.ToLocalTime().ToString("dd/MM/yyyy");
        }
        Console.WriteLine("[{0}] {1} | criada em {2:dd/MM/yyyy} | {3}",
            d.Id, d.Valor.ToString("C", cultura), d.DataCriacao.ToLocalTime(), situacao);
    }
    Console.WriteLine("==========================");
}

void PendurarDivida()
{
    var id = LerId("Id do cliente: ");
    if (id is null || !clienteService.Existe(id.Value))
    {
        Console.WriteLine("Cliente não encontrado.");
        return;
    }

    Console.Write("Valor: ");
    decimal.TryParse(Console.ReadLine(), NumberStyles.Number, cultura, out var valor);

    var divida = new Divida { Valor = valor };
    var sucesso = dividaService.Criar(id.Value, divida, out var erros);
    if (sucesso)
    {
        Console.WriteLine("Dívida de {0} registrada!", divida.Valor.ToString("C", cultura));
    }
    else
    {
        Console.WriteLine("Falha ao registrar a dívida");
        foreach (var erro in erros)
        {
            Console.WriteLine("{0}: {1}", erro.MemberNames.FirstOrDefault(), erro.ErrorMessage);
        }
    }
}

void PagarDivida()
{
    var clienteId = LerId("Id do cliente: ");
    var dividaId = LerId("Id da dívida: ");
    if (clienteId is null || dividaId is null)
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    var divida = dividaService.Obter(clienteId.Value, dividaId.Value);
    if (divida is null)
    {
        Console.WriteLine("Dívida não encontrada.");
        return;
    }

    var sucesso = dividaService.Pagar(divida, out var erros);
    if (sucesso)
    {
        Console.WriteLine("Dívida marcada como paga!");
    }
    else
    {
        foreach (var erro in erros)
        {
            Console.WriteLine(erro.ErrorMessage);
        }
    }
}

int? LerId(string mensagem)
{
    Console.Write(mensagem);
    return int.TryParse(Console.ReadLine(), out var id) ? id : null;
}
