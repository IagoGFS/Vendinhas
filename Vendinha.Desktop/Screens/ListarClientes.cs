using Vendinha.Services;

namespace Vendinha.Desktop.Screens
{
    public partial class ListarClientes : Form
    {
        private readonly ClienteService clienteService;
        private readonly DividaService dividaService;

        private int pagina = 1;
        private int totalPaginas = 1;

        public ListarClientes(ClienteService clienteService, DividaService dividaService)
        {
            this.clienteService = clienteService;
            this.dividaService = dividaService;
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            var resultado = clienteService.Listar(txtBusca.Text, pagina);
            totalPaginas = Math.Max(resultado.TotalPaginas, 1);

            // excluiu o último cliente da página: volta uma página
            if (pagina > totalPaginas)
            {
                pagina = totalPaginas;
                CarregarDados();
                return;
            }

            dataGridView1.Rows.Clear();
            foreach (var cliente in resultado.Itens)
            {
                dataGridView1.Rows.Add(
                    cliente.Id,
                    cliente.NomeCompleto,
                    FormatarCpf(cliente.Cpf),
                    cliente.Idade,
                    cliente.TotalEmAberto.ToString("C"),
                    cliente.TotalDividas.ToString("C")
                );
            }

            lblPagina.Text = $"Página {pagina} de {totalPaginas}";
            btnAnterior.Enabled = pagina > 1;
            btnProxima.Enabled = pagina < totalPaginas;
        }

        private static string FormatarCpf(string cpf)
        {
            if (cpf.Length != 11)
            {
                return cpf;
            }
            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }

        private int? IdSelecionado()
        {
            if (dataGridView1.CurrentRow is null)
            {
                return null;
            }
            return Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            pagina = 1;
            CarregarDados();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            pagina--;
            CarregarDados();
        }

        private void btnProxima_Click(object sender, EventArgs e)
        {
            pagina++;
            CarregarDados();
        }

        private void btnDividas_Click(object sender, EventArgs e)
        {
            var id = IdSelecionado();
            if (id is null)
            {
                MessageBox.Show("Selecione um cliente.");
                return;
            }

            var nome = dataGridView1.CurrentRow!.Cells[1].Value?.ToString() ?? string.Empty;
            var tela = new DividasCliente(dividaService, id.Value, nome);
            tela.ShowDialog(this);

            CarregarDados(); // os totais podem ter mudado
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var id = IdSelecionado();
            if (id is null)
            {
                MessageBox.Show("Selecione um cliente.");
                return;
            }

            var resposta = MessageBox.Show(
                "Excluir o cliente e todas as suas dívidas?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resposta != DialogResult.Yes)
            {
                return;
            }

            clienteService.Excluir(id.Value);
            CarregarDados();
        }
    }
}
