# Vendinha - Controle de contas penduradas

API em **.NET (C#)** para a vendinha controlar as dívidas ("penduras") dos clientes:
CRUD de clientes, cadastro de dívidas, listagem paginada ordenada por quem mais deve
e baixa (pagamento) de dívidas.

## Tecnologias

- C# / .NET 10 (compatível com .NET 8+ trocando o `TargetFramework` e as versões dos pacotes)
- ASP.NET Core Web API (Controllers)
- Windows Forms (desktop), console, servidor MCP e interface web em React + Vite, no mesmo padrão visto em aula
- Entity Framework Core + Npgsql (PostgreSQL)
- Data Annotations para validação

## Estrutura da solução

```
Vendinha.slnx
├── Vendinha/                  Regras de negócio + versão console (Program.cs)
│   ├── Models/                Cliente, Divida
│   ├── Validations/           CpfAttribute (validação de CPF)
│   ├── Contexts/              VendinhaDbContext (mapeamento EF Core)
│   ├── Dtos/                  Requests/Responses da API
│   └── Services/              ClienteService, DividaService
├── Vendinha.Api/              Web API
│   ├── Controllers/           ClienteController, DividaController
│   ├── Program.cs             Injeção de dependência e pipeline
│   └── appsettings.json       Connection string
├── Vendinha.Desktop/          Windows Forms (net10.0-windows)
│   ├── Form1.cs               Menu principal
│   └── Screens/               CadastroCliente, ListarClientes, DividasCliente
├── Vendinha.Mcp/              Servidor MCP (tools para uso por IA)
│   └── Tools/                 VendinhaTools
├── Vendinha.Web/              Interface React + Vite (CSS Modules + fetch)
│   └── src/                   App.jsx, api.js, components/
├── database/
│   ├── schema.sql             Script de criação do schema (PostgreSQL)
│   └── seed.sql               Dados de exemplo (opcional)
└── docs/API.md                Documentação dos endpoints
```

## Como executar

### Pré-requisitos
- .NET SDK 10 (ou 8+)
- PostgreSQL 14+ (pgAdmin ou `psql`)
- Visual Studio 2022/2026 (opcional)

### Passo a passo
1. Crie o banco: `CREATE DATABASE vendinha;`
2. Conectado ao banco `vendinha`, execute `database/schema.sql`.
   (Opcional: `database/seed.sql` para dados de exemplo.)
3. Ajuste a connection string em `Vendinha.Api/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Postgres": "Server=localhost;Port=5432;Database=vendinha;User Id=postgres;Password=SUA_SENHA;"
   }
   ```
4. Execute:
   ```bash
   dotnet run --project Vendinha.Api
   ```
   ou, no Visual Studio, defina `Vendinha.Api` como projeto de inicialização e pressione **F5**.
5. A API sobe em `http://localhost:5100`. Teste pelo arquivo `Vendinha.Api/Vendinha.Api.http`
   (Visual Studio) ou pelo Postman/Insomnia. O documento OpenAPI fica em `/openapi/v1.json`
   (somente em Development).

### Console, desktop, MCP e web (opcionais)

Além da API, a solução traz as mesmas funcionalidades em outras interfaces,
reaproveitando os mesmos `Services`.

- **Console:** `dotnet run --project Vendinha` (menu com listar, cadastrar, atualizar,
  excluir clientes, listar/pendurar/pagar dívidas).
- **Desktop (Windows):** `dotnet run --project Vendinha.Desktop` ou defina `Vendinha.Desktop`
  como projeto de inicialização no Visual Studio. Telas de cadastro, listagem com busca e
  paginação, e dívidas do cliente.

- **Web (React):** com a API rodando, `cd Vendinha.Web`, `npm install` e `npm run dev`
  (abre em `http://localhost:5173`). Veja `Vendinha.Web/README.md`.
- **MCP:** `dotnet run --project Vendinha.Mcp` expõe as tools (consultar/criar cliente,
  consultar/pendurar/pagar dívida) em `http://localhost:5200/mcp`.

Console e desktop não leem o `appsettings.json`: usam `VendinhaDbContext.ConnectionStringPadrao`
(localhost, banco `vendinha`, usuário `postgres`). Para trocar a senha/servidor sem alterar o
código, defina a variável de ambiente `VENDINHA_CONNECTION` com a connection string.

## Regras de negócio

- Nome completo, CPF e data de nascimento são obrigatórios; e-mail é opcional, mas validado se informado.
- O CPF é validado pelos dígitos verificadores, aceito com ou sem máscara e armazenado só com dígitos.
- Não pode haver dois clientes com o mesmo CPF (validação no serviço + `UNIQUE` no banco).
- A data de nascimento não pode estar no futuro; a **idade é calculada** a cada resposta.
- Um cliente só pode ter **uma dívida em aberto** (validação no serviço + índice único parcial no banco).
- A dívida é criada como "em aberto" com a data de criação definida pelo sistema; ao pagar,
  ela fica "paga" e recebe a data de pagamento.
- A listagem de clientes (`GET /api/cliente?pesquisa=&page=`) é paginada de 10 em 10, ordenada de quem
  mais deve para quem menos deve (desempate por nome) e filtra por parte do nome
  (sem diferenciar maiúsculas/minúsculas).
- `totalEmAberto` é o valor que o cliente deve hoje; `totalDividas` é a soma de todas as dívidas
  já registradas (abertas e pagas).
- Excluir um cliente exclui também as suas dívidas.

## Decisões de projeto

- Segue a arquitetura vista em aula: **Models → Contexts → Services → Controllers**, com serviços
  que validam por Data Annotations e devolvem `bool` + lista de erros (`out`).
- DTOs (`records`) separam o que a API recebe/devolve das entidades do banco.
- O schema é criado por script SQL (`database/schema.sql`), sem migrations.
- `Nullable` desativado e parâmetros `pesquisa`/`page`, como nas aulas mais recentes.
- A API libera CORS para o front (`localhost:5173`/`4173`) com a política `Frontend`.

## Documentação da API

Veja [docs/API.md](docs/API.md).
