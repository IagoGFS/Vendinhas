using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens
{
    public partial class DividasCliente : Form
    {
        private readonly DividaService service;
        private readonly int clienteId;

        public DividasCliente(DividaService service, int clienteId, string nomeCliente)
        {
            this.service = service;
            this.clienteId = clienteId;
            InitializeComponent();
            lblTitulo.Text = $"Dívidas de {nomeCliente}";
            CarregarDados();
        }

        private void CarregarDados()
        {
            var dividas = service.Listar(clienteId);

            dataGridView1.Rows.Clear();
            foreach (var divida in dividas)
            {
                dataGridView1.Rows.Add(
                    divida.Id,
                    divida.Valor.ToString("C"),
                    divida.Paga ? "Paga" : "Em aberto",
                    divida.DataCriacao.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    divida.DataPagamento.HasValue
                        ? divida.DataPagamento.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                        : ""
                );
            }
        }

        private void btnPendurar_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txtValor.Text, NumberStyles.Number,
                CultureInfo.CurrentCulture, out var valor);

            var divida = new Divida { Valor = valor };
            var sucesso = service.Criar(clienteId, divida, out var erros);

            if (sucesso)
            {
                txtValor.Clear();
                CarregarDados();
            }
            else
            {
                var mensagem = string.Join(Environment.NewLine, erros.Select(erro => erro.ErrorMessage));
                MessageBox.Show(mensagem, "Não foi possível pendurar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow is null)
            {
                MessageBox.Show("Selecione uma dívida.");
                return;
            }

            var dividaId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var divida = service.Obter(clienteId, dividaId);
            if (divida is null)
            {
                MessageBox.Show("Dívida não encontrada.");
                return;
            }

            var sucesso = service.Pagar(divida, out var erros);
            if (sucesso)
            {
                CarregarDados();
            }
            else
            {
                var mensagem = string.Join(Environment.NewLine, erros.Select(erro => erro.ErrorMessage));
                MessageBox.Show(mensagem, "Não foi possível marcar como paga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
