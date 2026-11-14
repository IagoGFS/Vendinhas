namespace Vendinha.Desktop.Screens
{
    partial class CadastroCliente
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
            label1 = new Label();
            txtNome = new TextBox();
            label2 = new Label();
            txtCpf = new TextBox();
            label3 = new Label();
            pickerDataNascimento = new DateTimePicker();
            label4 = new Label();
            txtEmail = new TextBox();
            btnCadastrar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(60, 30);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 0;
            label1.Text = "Nome completo";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(60, 53);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(360, 27);
            txtNome.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 95);
            label2.Name = "label2";
            label2.Size = new Size(36, 20);
            label2.TabIndex = 2;
            label2.Text = "CPF";
            // 
            // txtCpf
            // 
            txtCpf.Location = new Point(60, 118);
            txtCpf.Name = "txtCpf";
            txtCpf.Size = new Size(200, 27);
            txtCpf.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 160);
            label3.Name = "label3";
            label3.Size = new Size(145, 20);
            label3.TabIndex = 4;
            label3.Text = "Data de Nascimento";
            // 
            // pickerDataNascimento
            // 
            pickerDataNascimento.Format = DateTimePickerFormat.Short;
            pickerDataNascimento.Location = new Point(60, 183);
            pickerDataNascimento.Name = "pickerDataNascimento";
            pickerDataNascimento.Size = new Size(200, 27);
            pickerDataNascimento.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(60, 225);
            label4.Name = "label4";
            label4.Size = new Size(110, 20);
            label4.TabIndex = 6;
            label4.Text = "E-mail (opcional)";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(60, 248);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(360, 27);
            txtEmail.TabIndex = 7;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(60, 300);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(360, 60);
            btnCadastrar.TabIndex = 8;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // CadastroCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 400);
            Controls.Add(btnCadastrar);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(pickerDataNascimento);
            Controls.Add(label3);
            Controls.Add(txtCpf);
            Controls.Add(label2);
            Controls.Add(txtNome);
            Controls.Add(label1);
            Name = "CadastroCliente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cadastro de cliente";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNome;
        private Label label2;
        private TextBox txtCpf;
        private Label label3;
        private DateTimePicker pickerDataNascimento;
        private Label label4;
        private TextBox txtEmail;
        private Button btnCadastrar;
    }
}
