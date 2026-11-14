import { useEffect, useState } from 'react'
import { excluirCliente, listarClientes } from './api'
import { formatCpf, formatCurrency, formatDate } from './format'
import ClienteModal from './components/ClienteModal'
import DividasModal from './components/DividasModal'
import styles from './App.module.css'

function App() {
  const [clientes, setClientes] = useState([])
  const [pesquisa, setPesquisa] = useState('')
  const [page, setPage] = useState(1)
  const [totalPaginas, setTotalPaginas] = useState(1)
  const [totalItens, setTotalItens] = useState(0)
  const [refreshToken, setRefreshToken] = useState(0)
  const [loading, setLoading] = useState(true)
  const [loadError, setLoadError] = useState('')
  const [hasConnected, setHasConnected] = useState(false)

  // null = fechado | { cliente: null } = novo | { cliente } = edição
  const [clienteModal, setClienteModal] = useState(null)
  const [dividasCliente, setDividasCliente] = useState(null)

  useEffect(() => {
    const controller = new AbortController()
    const timeout = window.setTimeout(async () => {
      setLoading(true)
      setLoadError('')
      try {
        const resultado = await listarClientes({ pesquisa: pesquisa.trim(), page }, controller.signal)
        // excluiu o último cliente da página: volta uma página
        if (resultado.itens.length === 0 && page > 1) {
          setPage(Math.max(1, resultado.totalPaginas))
          return
        }
        setClientes(resultado.itens)
        setTotalPaginas(Math.max(1, resultado.totalPaginas))
        setTotalItens(resultado.totalItens)
        setHasConnected(true)
      } catch (error) {
        if (error.name !== 'AbortError') {
          setLoadError(error.message)
          setClientes([])
        }
      } finally {
        if (!controller.signal.aborted) setLoading(false)
      }
    }, pesquisa ? 250 : 0)

    return () => {
      window.clearTimeout(timeout)
      controller.abort()
    }
  }, [page, pesquisa, refreshToken])

  function recarregar() {
    setRefreshToken((token) => token + 1)
  }

  function handleClienteSalvo() {
    setClienteModal(null)
    recarregar()
  }

  async function handleExcluir(cliente) {
    const confirmou = window.confirm(
      `Excluir ${cliente.nomeCompleto}? As dívidas do cliente também serão excluídas.`,
    )
    if (!confirmou) return

    try {
      await excluirCliente(cliente.id)
      recarregar()
    } catch (error) {
      setLoadError(error.message)
    }
  }

  return (
    <div className={styles.app}>
      <header className={styles.navbar}>
        <a className={styles.brand} href="/" aria-label="Vendinha, início">
          <span className={styles.brandMark}>V</span>
          <span>vendinha<span className={styles.brandDot}>.</span></span>
        </a>
        <nav className={styles.navLinks} aria-label="Navegação principal">
          <a className={styles.navLinkActive} href="#clientes">Clientes</a>
        </nav>
        <div className={styles.connection}>
          <span className={`${styles.connectionDot} ${hasConnected ? styles.connected : ''}`} />
          <span>{hasConnected ? 'API conectada' : 'Conexão com a API'}</span>
        </div>
      </header>

      <main className={styles.main} id="clientes">
        <div className={styles.pageHeading}>
          <div>
            <p className={styles.eyebrow}>CONTAS PENDURADAS <span>/</span> CADASTROS</p>
            <h1>Clientes</h1>
            <p className={styles.subtitle}>Cadastre clientes e controle quem está devendo na vendinha.</p>
          </div>
          <button className={styles.primaryButton} type="button" onClick={() => setClienteModal({ cliente: null })}>
            <span aria-hidden="true">+</span>
            Novo cliente
          </button>
        </div>

        <section className={styles.panel} aria-labelledby="lista-title">
          <div className={styles.panelHeader}>
            <div>
              <h2 id="lista-title">Todos os clientes</h2>
              <p>Ordenados de quem mais deve para quem menos deve.</p>
            </div>
            <label className={styles.searchBox}>
              <span className={styles.searchIcon} aria-hidden="true">⌕</span>
              <span className={styles.visuallyHidden}>Pesquisar clientes</span>
              <input
                type="search"
                placeholder="Buscar pelo nome..."
                value={pesquisa}
                onChange={(event) => {
                  setPesquisa(event.target.value)
                  setPage(1)
                }}
              />
            </label>
          </div>

          {loadError && (
            <div className={styles.errorBanner} role="alert">
              <span>{loadError}</span>
              <button type="button" onClick={recarregar}>Tentar novamente</button>
            </div>
          )}

          <div className={styles.tableScroll}>
            <table className={styles.table}>
              <thead>
                <tr>
                  <th>CLIENTE</th>
                  <th>CPF</th>
                  <th>NASCIMENTO</th>
                  <th>IDADE</th>
                  <th>DEVE</th>
                  <th>TOTAL DE DÍVIDAS</th>
                  <th aria-label="Ações" />
                </tr>
              </thead>
              <tbody>
                {loading ? (
                  <tr><td className={styles.tableMessage} colSpan="7">Carregando clientes...</td></tr>
                ) : clientes.length === 0 ? (
                  <tr>
                    <td className={styles.tableMessage} colSpan="7">
                      {loadError
                        ? 'Não foi possível exibir os registros.'
                        : pesquisa ? 'Nenhum cliente encontrado.' : 'Ainda não há clientes cadastrados.'}
                    </td>
                  </tr>
                ) : clientes.map((cliente) => (
                  <tr key={cliente.id}>
                    <td>
                      <div className={styles.identity}>
                        <span className={styles.avatar}>{cliente.nomeCompleto.trim()[0]?.toUpperCase() || '?'}</span>
                        <div>
                          <span className={styles.name}>{cliente.nomeCompleto}</span>
                          <span className={styles.muted}>{cliente.email || 'sem e-mail'}</span>
                        </div>
                      </div>
                    </td>
                    <td className={styles.mono}>{formatCpf(cliente.cpf)}</td>
                    <td>{formatDate(cliente.dataNascimento)}</td>
                    <td>{cliente.idade} <span className={styles.muted}>anos</span></td>
                    <td className={`${styles.mono} ${cliente.totalEmAberto > 0 ? styles.owing : styles.zero}`}>
                      {formatCurrency(cliente.totalEmAberto)}
                    </td>
                    <td className={styles.mono}>{formatCurrency(cliente.totalDividas)}</td>
                    <td className={styles.actions}>
                      <button className={styles.linkButton} type="button" onClick={() => setDividasCliente(cliente)}>
                        Dívidas
                      </button>
                      <button className={styles.linkButton} type="button" onClick={() => setClienteModal({ cliente })}>
                        Editar
                      </button>
                      <button className={styles.dangerLink} type="button" onClick={() => handleExcluir(cliente)}>
                        Excluir
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <footer className={styles.tableFooter}>
            <span>{loading ? 'Carregando registros' : `${totalItens} ${totalItens === 1 ? 'cliente' : 'clientes'}`}</span>
            <div className={styles.pagination}>
              <button
                type="button"
                onClick={() => setPage((current) => Math.max(1, current - 1))}
                disabled={page <= 1 || loading}
                aria-label="Página anterior"
              >
                ←
              </button>
              <span>Página {page} de {totalPaginas}</span>
              <button
                type="button"
                onClick={() => setPage((current) => current + 1)}
                disabled={page >= totalPaginas || loading}
                aria-label="Próxima página"
              >
                →
              </button>
            </div>
          </footer>
        </section>

        <p className={styles.pageNote}>Os dados são fornecidos pela API da Vendinha.</p>
      </main>

      {clienteModal && (
        <ClienteModal
          cliente={clienteModal.cliente}
          onClose={() => setClienteModal(null)}
          onSaved={handleClienteSalvo}
        />
      )}

      {dividasCliente && (
        <DividasModal
          cliente={dividasCliente}
          onClose={() => setDividasCliente(null)}
          onChanged={recarregar}
        />
      )}
    </div>
  )
}

export default App
