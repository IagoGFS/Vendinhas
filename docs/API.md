# Documentação da API - Vendinha

Base URL local: `http://localhost:5100`. Todas as rotas aceitam e devolvem JSON.

Erros de validação devolvem **422** com a lista:

```json
[
  { "campo": "Cpf", "mensagem": "O CPF informado é inválido." }
]
```

## Clientes - `/api/cliente`

### POST /api/cliente - cadastrar
Request:
```json
{
  "nomeCompleto": "Maria da Silva",
  "cpf": "529.982.247-25",
  "dataNascimento": "1990-05-20",
  "email": "maria@email.com"
}
```
Resposta **201**:
```json
{
  "id": 1,
  "nomeCompleto": "Maria da Silva",
  "cpf": "52998224725",
  "dataNascimento": "1990-05-20",
  "idade": 36,
  "email": "maria@email.com",
  "totalDividas": 0,
  "totalEmAberto": 0
}
```
Erros: **400** (JSON inválido), **422** (nome/CPF/data ausentes ou inválidos, CPF duplicado, e-mail inválido).

### GET /api/cliente?pesquisa={texto}&page={n} - listar
- `pesquisa` (opcional): texto contido no nome (sem diferenciar maiúsculas/minúsculas).
- `page` (opcional, padrão 1): páginas de 10 itens.
- Ordenado de quem mais deve para quem menos deve.

Resposta **200**:
```json
{
  "itens": [
    {
      "id": 1,
      "nomeCompleto": "Maria da Silva",
      "cpf": "52998224725",
      "dataNascimento": "1990-05-20",
      "idade": 36,
      "email": "maria@email.com",
      "totalDividas": 45.90,
      "totalEmAberto": 45.90
    }
  ],
  "pagina": 1,
  "tamanhoPagina": 10,
  "totalItens": 1,
  "totalPaginas": 1
}
```

### GET /api/cliente/{id} - obter
**200** com o cliente ou **404**.

### PUT /api/cliente/{id} - atualizar
Mesmo corpo do POST. **200** com o cliente atualizado, **404** ou **422**.

### DELETE /api/cliente/{id} - excluir
**204** (as dívidas do cliente também são excluídas) ou **404**.

## Dívidas - `/api/cliente/{clienteId}/divida`

### POST - pendurar uma dívida
Request:
```json
{ "valor": 45.90 }
```
Resposta **201**:
```json
{
  "id": 1,
  "clienteId": 1,
  "valor": 45.90,
  "paga": false,
  "situacao": "Em aberto",
  "dataCriacao": "2026-11-12T15:30:00Z",
  "dataPagamento": null
}
```
Erros: **404** (cliente inexistente), **422** (valor <= 0 ou cliente já tem dívida em aberto).

### GET - listar dívidas do cliente
**200** com a lista (mais recentes primeiro) ou **404** se o cliente não existe.

### GET /{dividaId} - obter uma dívida
**200** ou **404**.

### POST /{dividaId}/pagar - marcar como paga
**200** com `paga: true` e `dataPagamento` preenchida; **404** ou **422** (já estava paga).
