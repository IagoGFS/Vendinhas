const BASE_URL = (import.meta.env.VITE_API_URL || 'http://localhost:5100/api').replace(/\/$/, '')

class ApiError extends Error {}

async function readPayload(response) {
  if (response.status === 204) return null
  const contentType = response.headers.get('content-type') || ''
  if (contentType.includes('json')) return response.json()
  return response.text()
}

function messageFrom(response, payload) {
  // 422: lista de { campo, mensagem } vinda das validações do serviço
  if (Array.isArray(payload)) {
    const mensagens = payload.map((item) => item.mensagem).filter(Boolean)
    if (mensagens.length) return mensagens.join(' ')
  }
  if (response.status === 404) return 'Registro não encontrado.'
  if (payload && payload.title) return 'Dados inválidos. Confira os campos e tente novamente.'
  return 'Não foi possível concluir a operação.'
}

async function request(path, { signal, method = 'GET', body } = {}) {
  let response
  try {
    response = await fetch(`${BASE_URL}${path}`, {
      method,
      signal,
      headers: body ? { 'Content-Type': 'application/json' } : undefined,
      body: body ? JSON.stringify(body) : undefined,
    })
  } catch (error) {
    if (error.name === 'AbortError') throw error
    throw new ApiError('Falha ao conectar com a API.')
  }

  const payload = await readPayload(response)
  if (!response.ok) throw new ApiError(messageFrom(response, payload))
  return payload
}

// ---- Clientes ----
export function listarClientes({ pesquisa, page }, signal) {
  const params = new URLSearchParams({ page: String(page) })
  if (pesquisa) params.set('pesquisa', pesquisa)
  return request(`/cliente?${params.toString()}`, { signal })
}

export function criarCliente(dados) {
  return request('/cliente', { method: 'POST', body: dados })
}

export function atualizarCliente(id, dados) {
  return request(`/cliente/${id}`, { method: 'PUT', body: dados })
}

export function excluirCliente(id) {
  return request(`/cliente/${id}`, { method: 'DELETE' })
}

// ---- Dívidas ----
export function listarDividas(clienteId, signal) {
  return request(`/cliente/${clienteId}/divida`, { signal })
}

export function pendurarDivida(clienteId, valor) {
  return request(`/cliente/${clienteId}/divida`, { method: 'POST', body: { valor } })
}

export function pagarDivida(clienteId, dividaId) {
  return request(`/cliente/${clienteId}/divida/${dividaId}/pagar`, { method: 'POST' })
}
