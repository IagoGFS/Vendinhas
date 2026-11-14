# Vendinha Web

Interface React + Vite (CSS Modules + fetch API) para a `Vendinha.Api`: clientes e dívidas
na mesma tela, com tabela e modais.

## Executar

Na raiz da solução, inicie a API:

```powershell
dotnet run --project .\Vendinha.Api
```

Em outro terminal:

```powershell
cd .\Vendinha.Web
npm install
npm run dev
```

A interface usa `http://localhost:5100/api` por padrão. Para apontar para outra API, crie o
arquivo `.env.local`:

```env
VITE_API_URL=http://localhost:5100/api
```

A API libera as origens locais do Vite (`localhost` e `127.0.0.1`, portas `5173` e `4173`) via CORS.

## O que a tela faz

- Lista de clientes (10 por página), do que mais deve para o que menos deve, com pesquisa por nome.
- Novo cliente, edição e exclusão (modal).
- Dívidas do cliente (modal): pendurar uma nova dívida e marcar como paga.
