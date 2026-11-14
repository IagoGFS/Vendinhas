using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens
{
    public partial class CadastroCliente : Form
    {
        private readonly ClienteService service;

        public CadastroCliente(ClienteService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente
            {
                NomeCompleto = txtNome.Text,
                Cpf = txtCpf.Text,
                DataNascimento = DateOnly.FromDateTime(pickerDataNascimento.Value),
                Email = txtEmail.Text
            };

            var sucesso = service.Criar(cliente, out var erros);

            if (sucesso)
            {
                MessageBox.Show("Cliente cadastrado com sucesso");
                this.Close();
            }
            else
            {
                var mensagem = string.Join(Environment.NewLine, erros.Select(erro => erro.ErrorMessage));
                MessageBox.Show(mensagem, "Falha no cadastro do cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
