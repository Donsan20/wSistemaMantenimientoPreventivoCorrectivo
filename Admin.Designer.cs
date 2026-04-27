namespace wSistemaMantenimientoPreventivoCorrectivo
{
    partial class Admin
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFiltrar = new System.Windows.Forms.ComboBox();
            this.pnlAlertas = new System.Windows.Forms.Panel();
            this.lblAlertas = new System.Windows.Forms.Label();
            this.Advertencia = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRolSesion = new System.Windows.Forms.Label();
            this.btnNuevaOrden = new System.Windows.Forms.Button();
            this.btnEquipos = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnlAlertas.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(277, 122);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(541, 276);
            this.dataGridView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(270, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "DASHBOARD DE CONTROL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(594, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filtrar";
            // 
            // cmbFiltrar
            // 
            this.cmbFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltrar.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFiltrar.FormattingEnabled = true;
            this.cmbFiltrar.Location = new System.Drawing.Point(674, 11);
            this.cmbFiltrar.Name = "cmbFiltrar";
            this.cmbFiltrar.Size = new System.Drawing.Size(142, 28);
            this.cmbFiltrar.TabIndex = 3;
            this.cmbFiltrar.SelectedIndexChanged += new System.EventHandler(this.cmbFiltrar_SelectedIndexChanged_1);
            // 
            // pnlAlertas
            // 
            this.pnlAlertas.BackColor = System.Drawing.Color.Tomato;
            this.pnlAlertas.Controls.Add(this.lblAlertas);
            this.pnlAlertas.Controls.Add(this.Advertencia);
            this.pnlAlertas.Location = new System.Drawing.Point(277, 46);
            this.pnlAlertas.Name = "pnlAlertas";
            this.pnlAlertas.Size = new System.Drawing.Size(541, 62);
            this.pnlAlertas.TabIndex = 4;
            // 
            // lblAlertas
            // 
            this.lblAlertas.BackColor = System.Drawing.Color.Red;
            this.lblAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlertas.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertas.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAlertas.Location = new System.Drawing.Point(0, 0);
            this.lblAlertas.Name = "lblAlertas";
            this.lblAlertas.Size = new System.Drawing.Size(541, 62);
            this.lblAlertas.TabIndex = 1;
            this.lblAlertas.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Advertencia
            // 
            this.Advertencia.BackColor = System.Drawing.Color.Red;
            this.Advertencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Advertencia.Location = new System.Drawing.Point(0, 0);
            this.Advertencia.Name = "Advertencia";
            this.Advertencia.Size = new System.Drawing.Size(541, 62);
            this.Advertencia.TabIndex = 0;
            this.Advertencia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel2.Controls.Add(this.lblRolSesion);
            this.panel2.Controls.Add(this.btnNuevaOrden);
            this.panel2.Controls.Add(this.btnEquipos);
            this.panel2.Controls.Add(this.btnReportes);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnDashboard);
            this.panel2.Location = new System.Drawing.Point(0, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 413);
            this.panel2.TabIndex = 5;
            // 
            // lblRolSesion
            // 
            this.lblRolSesion.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRolSesion.Location = new System.Drawing.Point(27, 48);
            this.lblRolSesion.Name = "lblRolSesion";
            this.lblRolSesion.Size = new System.Drawing.Size(216, 58);
            this.lblRolSesion.TabIndex = 10;
            this.lblRolSesion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnNuevaOrden
            // 
            this.btnNuevaOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaOrden.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNuevaOrden.Location = new System.Drawing.Point(32, 196);
            this.btnNuevaOrden.Name = "btnNuevaOrden";
            this.btnNuevaOrden.Size = new System.Drawing.Size(211, 23);
            this.btnNuevaOrden.TabIndex = 9;
            this.btnNuevaOrden.Text = "NUEVA ORDEN";
            this.btnNuevaOrden.UseVisualStyleBackColor = true;
            // 
            // btnEquipos
            // 
            this.btnEquipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEquipos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEquipos.Location = new System.Drawing.Point(32, 167);
            this.btnEquipos.Name = "btnEquipos";
            this.btnEquipos.Size = new System.Drawing.Size(211, 23);
            this.btnEquipos.TabIndex = 8;
            this.btnEquipos.Text = "EQUIPOS";
            this.btnEquipos.UseVisualStyleBackColor = true;
            // 
            // btnReportes
            // 
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReportes.Location = new System.Drawing.Point(32, 138);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(211, 23);
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Text = "REPORTES";
            this.btnReportes.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "MANTENIMIENTO";
            // 
            // btnDashboard
            // 
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDashboard.Location = new System.Drawing.Point(32, 109);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(211, 23);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "DASHBOARD";
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 410);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlAlertas);
            this.Controls.Add(this.cmbFiltrar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Name = "Admin";
            this.Text = "Admin";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnlAlertas.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFiltrar;
        private System.Windows.Forms.Panel pnlAlertas;
        private System.Windows.Forms.Label Advertencia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnNuevaOrden;
        private System.Windows.Forms.Button btnEquipos;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Label lblAlertas;
        private System.Windows.Forms.Label lblRolSesion;
    }
}