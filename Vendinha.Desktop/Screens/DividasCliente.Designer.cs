namespace Vendinha.Desktop.Screens
{
    partial class DividasCliente
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
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colValor = new DataGridViewTextBoxColumn();
            colSituacao = new DataGridViewTextBoxColumn();
            colCriacao = new DataGridViewTextBoxColumn();
            colPagamento = new DataGridViewTextBoxColumn();
            lblValor = new Label();
            txtValor = new TextBox();
            btnPendurar = new Button();
            btnPagar = new Button();
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
            lblTitulo.Text = "Dívidas";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colValor, colSituacao, colCriacao, colPagamento });
            dataGridView1.Location = new Point(20, 70);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(740, 250);
            dataGridView1.TabIndex = 1;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 60;
            // 
            // colValor
            // 
            colValor.HeaderText = "Valor";
            colValor.MinimumWidth = 6;
            colValor.Name = "colValor";
            colValor.ReadOnly = true;
            colValor.Width = 120;
            // 
            // colSituacao
            // 
            colSituacao.HeaderText = "Situação";
            colSituacao.MinimumWidth = 6;
            colSituacao.Name = "colSituacao";
            colSituacao.ReadOnly = true;
            colSituacao.Width = 110;
            // 
            // colCriacao
            // 
            colCriacao.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCriacao.HeaderText = "Criada em";
            colCriacao.MinimumWidth = 6;
            colCriacao.Name = "colCriacao";
            colCriacao.ReadOnly = true;
            // 
            // colPagamento
            // 
            colPagamento.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPagamento.HeaderText = "Paga em";
            colPagamento.MinimumWidth = 6;
            colPagamento.Name = "colPagamento";
            colPagamento.ReadOnly = true;
            // 
            // lblValor
            // 
            lblValor.AutoSize = true;
            lblValor.Location = new Point(20, 345);
            lblValor.Name = "lblValor";
            lblValor.Size = new Size(42, 20);
            lblValor.TabIndex = 2;
            lblValor.Text = "Valor";
            // 
            // txtValor
            // 
            txtValor.Location = new Point(20, 368);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(150, 27);
            txtValor.TabIndex = 3;
            // 
            // btnPendurar
            // 
            btnPendurar.Location = new Point(185, 366);
            btnPendurar.Name = "btnPendurar";
            btnPendurar.Size = new Size(150, 32);
            btnPendurar.TabIndex = 4;
            btnPendurar.Text = "Pendurar dívida";
            btnPendurar.UseVisualStyleBackColor = true;
            btnPendurar.Click += btnPendurar_Click;
            // 
            // btnPagar
            // 
            btnPagar.Location = new Point(560, 366);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(200, 32);
            btnPagar.TabIndex = 5;
            btnPagar.Text = "Marcar como paga";
            btnPagar.UseVisualStyleBackColor = true;
            btnPagar.Click += btnPagar_Click;
            // 
            // DividasCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 420);
            Controls.Add(btnPagar);
            Controls.Add(btnPendurar);
            Controls.Add(txtValor);
            Controls.Add(lblValor);
            Controls.Add(dataGridView1);
            Controls.Add(lblTitulo);
            Name = "DividasCliente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Dívidas do cliente";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colValor;
        private DataGridViewTextBoxColumn colSituacao;
        private DataGridViewTextBoxColumn colCriacao;
        private DataGridViewTextBoxColumn colPagamento;
        private Label lblValor;
        private TextBox txtValor;
        private Button btnPendurar;
        private Button btnPagar;
    }
}
