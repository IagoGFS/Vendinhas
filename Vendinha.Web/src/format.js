const currency = new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' })

export function formatCurrency(value) {
  return currency.format(Number(value) || 0)
}

export function formatCpf(cpf) {
  if (!cpf || cpf.length !== 11) return cpf || '—'
  return `${cpf.slice(0, 3)}.${cpf.slice(3, 6)}.${cpf.slice(6, 9)}-${cpf.slice(9)}`
}

// A API devolve a data de nascimento como "AAAA-MM-DD" (sem fuso)
export function formatDate(value) {
  if (!value) return '—'
  const [ano, mes, dia] = value.split('-')
  return `${dia}/${mes}/${ano}`
}

// Datas das dívidas chegam em UTC (ISO 8601)
export function formatDateTime(value) {
  if (!value) return '—'
  const date = new Date(value)
  return Number.isNaN(date.getTime())
    ? '—'
    : new Intl.DateTimeFormat('pt-BR', { dateStyle: 'short', timeStyle: 'short' }).format(date)
}

// Aceita "45,90" e "45.90"
export function parseValor(text) {
  return Number(String(text).trim().replace(',', '.'))
}
