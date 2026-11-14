namespace Vendinha.Desktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            btnCadastrar = new Button();
            btnListar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 24F);
            lblTitulo.Location = new Point(150, 40);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(162, 54);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Vendinha";
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(110, 140);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(300, 70);
            btnCadastrar.TabIndex = 1;
            btnCadastrar.Text = "Cadastrar cliente";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // btnListar
            // 
            btnListar.Location = new Point(110, 230);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(300, 70);
            btnListar.TabIndex = 2;
            btnListar.Text = "Clientes e dívidas";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 360);
            Controls.Add(btnListar);
            Controls.Add(btnCadastrar);
            Controls.Add(lblTitulo);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vendinha";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnCadastrar;
        private Button btnListar;
    }
}
