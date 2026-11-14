using Vendinha.Desktop.Screens;
using Vendinha.Services;

namespace Vendinha.Desktop
{
    public partial class Form1 : Form
    {
        private readonly ClienteService clienteService;
        private readonly DividaService dividaService;

        public Form1()
        {
            clienteService = new ClienteService();
            dividaService = new DividaService();
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var tela = new CadastroCliente(clienteService);
            tela.ShowDialog(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            var tela = new ListarClientes(clienteService, dividaService);
            tela.ShowDialog(this);
        }
    }
}
