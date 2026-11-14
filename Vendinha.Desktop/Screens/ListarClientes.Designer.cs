namespace Vendinha.Desktop.Screens
{
    partial class ListarClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            txtBusca = new TextBox();
            btnBuscar = new Button();
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNome = new DataGridViewTextBoxColumn();
            colCpf = new DataGridViewTextBoxColumn();
            colIdade = new DataGridViewTextBoxColumn();
            colEmAberto = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            btnAnterior = new Button();
            lblPagina = new Label();
            btnProxima = new Button();
            btnDividas = new Button();
            btnExcluir = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F);
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(107, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Clientes";
            // 
            // txtBusca
            // 
            txtBusca.Location = new Point(20, 62);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Buscar por nome";
            txtBusca.Size = new Size(400, 27);
            txtBusca.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(430, 60);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colNome, colCpf, colIdade, colEmAberto, colTotal });
            dataGridView1.Location = new Point(20, 105);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(840, 300);
            dataGridView1.TabIndex = 3;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 60;
            // 
            // colNome
            // 
            colNome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNome.HeaderText = "Nome";
            colNome.MinimumWidth = 6;
            colNome.Name = "colNome";
            colNome.ReadOnly = true;
            // 
            // colCpf
            // 
            colCpf.HeaderText = "CPF";
            colCpf.MinimumWidth = 6;
            colCpf.Name = "colCpf";
            colCpf.ReadOnly = true;
            colCpf.Width = 130;
            // 
            // colIdade
            // 
            colIdade.HeaderText = "Idade";
            colIdade.MinimumWidth = 6;
            colIdade.Name = "colIdade";
            colIdade.ReadOnly = true;
            colIdade.Width = 70;
            // 
            // colEmAberto
            // 
            colEmAberto.HeaderText = "Deve (em aberto)";
            colEmAberto.MinimumWidth = 6;
            colEmAberto.Name = "colEmAberto";
            colEmAberto.ReadOnly = true;
            colEmAberto.Width = 140;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "Total de dívidas";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            colTotal.Width = 140;
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(20, 420);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(110, 32);
            btnAnterior.TabIndex = 4;
            btnAnterior.Text = "< Anterior";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // lblPagina
            // 
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(145, 426);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(96, 20);
            lblPagina.TabIndex = 5;
            lblPagina.Text = "Página 1 de 1";
            // 
            // btnProxima
            // 
            btnProxima.Location = new Point(270, 420);
            btnProxima.Name = "btnProxima";
            btnProxima.Size = new Size(110, 32);
            btnProxima.TabIndex = 6;
            btnProxima.Text = "Próxima >";
            btnProxima.UseVisualStyleBackColor = true;
            btnProxima.Click += btnProxima_Click;
            // 
            // btnDividas
            // 
            btnDividas.Location = new Point(560, 420);
            btnDividas.Name = "btnDividas";
            btnDividas.Size = new Size(150, 32);
            btnDividas.TabIndex = 7;
            btnDividas.Text = "Ver dívidas";
            btnDividas.UseVisualStyleBackColor = true;
            btnDividas.Click += btnDividas_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(720, 420);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(140, 32);
            btnExcluir.TabIndex = 8;
            btnExcluir.Text = "Excluir cliente";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // ListarClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 470);
            Controls.Add(btnExcluir);
            Controls.Add(btnDividas);
            Controls.Add(btnProxima);
            Controls.Add(lblPagina);
            Controls.Add(btnAnterior);
            Controls.Add(dataGridView1);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusca);
            Controls.Add(lblTitulo);
            Name = "ListarClientes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtBusca;
        private Button btnBuscar;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNome;
        private DataGridViewTextBoxColumn colCpf;
        private DataGridViewTextBoxColumn colIdade;
        private DataGridViewTextBoxColumn colEmAberto;
        private DataGridViewTextBoxColumn colTotal;
        private Button btnAnterior;
        private Label lblPagina;
        private Button btnProxima;
        private Button btnDividas;
        private Button btnExcluir;
    }
}
