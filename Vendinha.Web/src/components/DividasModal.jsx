import { useCallback, useEffect, useState } from 'react'
import { listarDividas, pagarDivida, pendurarDivida } from '../api'
import { formatCurrency, formatDateTime, parseValor } from '../format'
import styles from '../App.module.css'

function DividasModal({ cliente, onClose, onChanged }) {
  const [dividas, setDividas] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const [valor, setValor] = useState('')
  const [saving, setSaving] = useState(false)

  const carregar = useCallback(async (signal) => {
    setLoading(true)
    try {
      const dados = await listarDividas(cliente.id, signal)
      setDividas(dados)
      setError('')
    } catch (err) {
      if (err.name !== 'AbortError') setError(err.message)
    } finally {
      if (!signal?.aborted) setLoading(false)
    }
  }, [cliente.id])

  useEffect(() => {
    const controller = new AbortController()
    carregar(controller.signal)
    return () => controller.abort()
  }, [carregar])

  useEffect(() => {
    function handleKeyDown(event) {
      if (event.key === 'Escape' && !saving) onClose()
    }
    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [saving, onClose])

  const emAberto = dividas.find((d) => !d.paga)

  async function executar(acao) {
    setError('')
    setSaving(true)
    try {
      await acao()
      await carregar()
      onChanged() // atualiza os totais na tabela de clientes
    } catch (err) {
      setError(err.message)
    } finally {
      setSaving(false)
    }
  }

  function handlePendurar(event) {
    event.preventDefault()
    executar(async () => {
      await pendurarDivida(cliente.id, parseValor(valor))
      setValor('')
    })
  }

  return (
    <div className={styles.modalBackdrop}>
      <section className={`${styles.modal} ${styles.modalWide}`} role="dialog" aria-modal="true" aria-labelledby="dividas-title">
        <div className={styles.modalHeader}>
          <div>
            <p className={styles.eyebrow}>DÍVIDAS</p>
            <h2 id="dividas-title">{cliente.nomeCompleto}</h2>
          </div>
          <button className={styles.closeButton} type="button" onClick={onClose} disabled={saving} aria-label="Fechar">
            ×
          </button>
        </div>

        <p className={styles.modalDescription}>
          {emAberto
            ? `Em aberto: ${formatCurrency(emAberto.valor)}. Só é possível pendurar outra dívida depois de pagar esta.`
            : 'Nenhuma dívida em aberto.'}
        </p>

        <form className={styles.inlineForm} onSubmit={handlePendurar}>
          <label>
            Valor (R$)
            <input
              required
              inputMode="decimal"
              value={valor}
              onChange={(event) => setValor(event.target.value)}
              placeholder="0,00"
              disabled={Boolean(emAberto) || saving}
            />
          </label>
          <button className={styles.primaryButton} type="submit" disabled={Boolean(emAberto) || saving}>
            Pendurar dívida
          </button>
        </form>

        {error && <p className={styles.formError} role="alert">{error}</p>}

        <div className={styles.tableScroll}>
          <table className={styles.table}>
            <thead>
              <tr>
                <th>VALOR</th>
                <th>SITUAÇÃO</th>
                <th>CRIADA EM</th>
                <th>PAGA EM</th>
                <th aria-label="Ações" />
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr><td className={styles.tableMessage} colSpan="5">Carregando dívidas...</td></tr>
              ) : dividas.length === 0 ? (
                <tr><td className={styles.tableMessage} colSpan="5">Este cliente ainda não tem dívidas.</td></tr>
              ) : dividas.map((divida) => (
                <tr key={divida.id}>
                  <td className={styles.mono}>{formatCurrency(divida.valor)}</td>
                  <td>
                    <span className={divida.paga ? styles.badgePaid : styles.badgeOpen}>{divida.situacao}</span>
                  </td>
                  <td>{formatDateTime(divida.dataCriacao)}</td>
                  <td>{formatDateTime(divida.dataPagamento)}</td>
                  <td className={styles.actions}>
                    {!divida.paga && (
                      <button
                        className={styles.linkButton}
                        type="button"
                        disabled={saving}
                        onClick={() => executar(() => pagarDivida(cliente.id, divida.id))}
                      >
                        Marcar como paga
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  )
}

export default DividasModal
