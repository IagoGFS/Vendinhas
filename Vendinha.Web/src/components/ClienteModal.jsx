import { useEffect, useState } from 'react'
import { atualizarCliente, criarCliente } from '../api'
import styles from '../App.module.css'

// cliente = null  -> novo cadastro | cliente = {...} -> edição
function ClienteModal({ cliente, onClose, onSaved }) {
  const editando = Boolean(cliente)
  const [form, setForm] = useState({
    nomeCompleto: cliente?.nomeCompleto ?? '',
    cpf: cliente?.cpf ?? '',
    dataNascimento: cliente?.dataNascimento ?? '',
    email: cliente?.email ?? '',
  })
  const [saving, setSaving] = useState(false)
  const [formError, setFormError] = useState('')

  useEffect(() => {
    function handleKeyDown(event) {
      if (event.key === 'Escape' && !saving) onClose()
    }
    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [saving, onClose])

  function handleChange(field) {
    return (event) => setForm({ ...form, [field]: event.target.value })
  }

  async function handleSubmit(event) {
    event.preventDefault()
    setFormError('')
    setSaving(true)
    try {
      if (editando) {
        await atualizarCliente(cliente.id, form)
      } else {
        await criarCliente(form)
      }
      onSaved()
    } catch (error) {
      setFormError(error.message)
    } finally {
      setSaving(false)
    }
  }

  return (
    <div className={styles.modalBackdrop}>
      <section className={styles.modal} role="dialog" aria-modal="true" aria-labelledby="cliente-title">
        <div className={styles.modalHeader}>
          <div>
            <p className={styles.eyebrow}>{editando ? 'EDIÇÃO' : 'NOVO CADASTRO'}</p>
            <h2 id="cliente-title">{editando ? 'Editar cliente' : 'Adicionar cliente'}</h2>
          </div>
          <button className={styles.closeButton} type="button" onClick={onClose} disabled={saving} aria-label="Fechar">
            ×
          </button>
        </div>

        <form className={styles.form} onSubmit={handleSubmit}>
          <label>
            Nome completo
            <input
              autoFocus
              required
              maxLength="150"
              value={form.nomeCompleto}
              onChange={handleChange('nomeCompleto')}
              placeholder="Nome do cliente"
            />
          </label>
          <label>
            CPF
            <input
              required
              maxLength="14"
              inputMode="numeric"
              value={form.cpf}
              onChange={handleChange('cpf')}
              placeholder="000.000.000-00"
            />
          </label>
          <label>
            Data de nascimento
            <input required type="date" value={form.dataNascimento} onChange={handleChange('dataNascimento')} />
          </label>
          <label>
            E-mail (opcional)
            <input
              type="email"
              maxLength="150"
              value={form.email}
              onChange={handleChange('email')}
              placeholder="cliente@exemplo.com"
            />
          </label>

          {formError && <p className={styles.formError} role="alert">{formError}</p>}

          <div className={styles.formActions}>
            <button className={styles.secondaryButton} type="button" onClick={onClose} disabled={saving}>
              Cancelar
            </button>
            <button className={styles.primaryButton} type="submit" disabled={saving}>
              {saving ? 'Salvando...' : editando ? 'Salvar alterações' : 'Cadastrar cliente'}
            </button>
          </div>
        </form>
      </section>
    </div>
  )
}

export default ClienteModal
