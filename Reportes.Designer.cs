namespace wSistemaMantenimientoPreventivoCorrectivo
{
    partial class Reportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabHistorico = new System.Windows.Forms.TabPage();
            this.lblPromedioDias = new System.Windows.Forms.Label();
            this.btnHistorico = new System.Windows.Forms.Button();
            this.lblEquipoReporte = new System.Windows.Forms.Label();
            this.cmbEquipoReporte = new System.Windows.Forms.ComboBox();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.tabResumenTecnico = new System.Windows.Forms.TabPage();
            this.btnResumenTecnico = new System.Windows.Forms.Button();
            this.dgvResumenTecnico = new System.Windows.Forms.DataGridView();
            this.tabOrdenesEstado = new System.Windows.Forms.TabPage();
            this.btnOrdenesEstado = new System.Windows.Forms.Button();
            this.dgvOrdenesEstado = new System.Windows.Forms.DataGridView();
            this.tabPorTipo = new System.Windows.Forms.TabPage();
            this.btnPorTipo = new System.Windows.Forms.Button();
            this.dgvPorTipo = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabHistorico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.tabResumenTecnico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenTecnico)).BeginInit();
            this.tabOrdenesEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesEstado)).BeginInit();
            this.tabPorTipo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorTipo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(230, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reportes del Sistema";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabHistorico);
            this.tabControl.Controls.Add(this.tabResumenTecnico);
            this.tabControl.Controls.Add(this.tabOrdenesEstado);
            this.tabControl.Controls.Add(this.tabPorTipo);
            this.tabControl.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(12, 45);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(750, 450);
            this.tabControl.TabIndex = 1;
            // 
            // tabHistorico
            // 
            this.tabHistorico.Controls.Add(this.lblPromedioDias);
            this.tabHistorico.Controls.Add(this.btnHistorico);
            this.tabHistorico.Controls.Add(this.lblEquipoReporte);
            this.tabHistorico.Controls.Add(this.cmbEquipoReporte);
            this.tabHistorico.Controls.Add(this.dgvHistorico);
            this.tabHistorico.Location = new System.Drawing.Point(4, 26);
            this.tabHistorico.Name = "tabHistorico";
            this.tabHistorico.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistorico.Size = new System.Drawing.Size(742, 420);
            this.tabHistorico.TabIndex = 0;
            this.tabHistorico.Text = "Histórico por Equipo";
            this.tabHistorico.UseVisualStyleBackColor = true;
            // 
            // lblPromedioDias
            // 
            this.lblPromedioDias.AutoSize = true;
            this.lblPromedioDias.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromedioDias.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPromedioDias.Location = new System.Drawing.Point(10, 380);
            this.lblPromedioDias.Name = "lblPromedioDias";
            this.lblPromedioDias.Size = new System.Drawing.Size(284, 17);
            this.lblPromedioDias.TabIndex = 4;
            this.lblPromedioDias.Text = "Seleccione un equipo para ver el promedio.";
            // 
            // btnHistorico
            // 
            this.btnHistorico.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorico.Location = new System.Drawing.Point(340, 15);
            this.btnHistorico.Name = "btnHistorico";
            this.btnHistorico.Size = new System.Drawing.Size(120, 28);
            this.btnHistorico.TabIndex = 3;
            this.btnHistorico.Text = "Generar Reporte";
            this.btnHistorico.UseVisualStyleBackColor = true;
            this.btnHistorico.Click += new System.EventHandler(this.btnHistorico_Click);
            // 
            // lblEquipoReporte
            // 
            this.lblEquipoReporte.AutoSize = true;
            this.lblEquipoReporte.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEquipoReporte.Location = new System.Drawing.Point(10, 20);
            this.lblEquipoReporte.Name = "lblEquipoReporte";
            this.lblEquipoReporte.Size = new System.Drawing.Size(52, 17);
            this.lblEquipoReporte.TabIndex = 2;
            this.lblEquipoReporte.Text = "Equipo:";
            // 
            // cmbEquipoReporte
            // 
            this.cmbEquipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipoReporte.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEquipoReporte.FormattingEnabled = true;
            this.cmbEquipoReporte.Location = new System.Drawing.Point(68, 17);
            this.cmbEquipoReporte.Name = "cmbEquipoReporte";
            this.cmbEquipoReporte.Size = new System.Drawing.Size(250, 25);
            this.cmbEquipoReporte.TabIndex = 1;
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorico.Location = new System.Drawing.Point(10, 55);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.Size = new System.Drawing.Size(720, 315);
            this.dgvHistorico.TabIndex = 0;
            // 
            // tabResumenTecnico
            // 
            this.tabResumenTecnico.Controls.Add(this.btnResumenTecnico);
            this.tabResumenTecnico.Controls.Add(this.dgvResumenTecnico);
            this.tabResumenTecnico.Location = new System.Drawing.Point(4, 26);
            this.tabResumenTecnico.Name = "tabResumenTecnico";
            this.tabResumenTecnico.Padding = new System.Windows.Forms.Padding(3);
            this.tabResumenTecnico.Size = new System.Drawing.Size(742, 420);
            this.tabResumenTecnico.TabIndex = 1;
            this.tabResumenTecnico.Text = "Resumen por Técnico";
            this.tabResumenTecnico.UseVisualStyleBackColor = true;
            // 
            // btnResumenTecnico
            // 
            this.btnResumenTecnico.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResumenTecnico.Location = new System.Drawing.Point(10, 10);
            this.btnResumenTecnico.Name = "btnResumenTecnico";
            this.btnResumenTecnico.Size = new System.Drawing.Size(150, 28);
            this.btnResumenTecnico.TabIndex = 1;
            this.btnResumenTecnico.Text = "Generar Reporte";
            this.btnResumenTecnico.UseVisualStyleBackColor = true;
            this.btnResumenTecnico.Click += new System.EventHandler(this.btnResumenTecnico_Click);
            // 
            // dgvResumenTecnico
            // 
            this.dgvResumenTecnico.AllowUserToAddRows = false;
            this.dgvResumenTecnico.AllowUserToDeleteRows = false;
            this.dgvResumenTecnico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumenTecnico.Location = new System.Drawing.Point(10, 50);
            this.dgvResumenTecnico.Name = "dgvResumenTecnico";
            this.dgvResumenTecnico.ReadOnly = true;
            this.dgvResumenTecnico.Size = new System.Drawing.Size(720, 360);
            this.dgvResumenTecnico.TabIndex = 0;
            // 
            // tabOrdenesEstado
            // 
            this.tabOrdenesEstado.Controls.Add(this.btnOrdenesEstado);
            this.tabOrdenesEstado.Controls.Add(this.dgvOrdenesEstado);
            this.tabOrdenesEstado.Location = new System.Drawing.Point(4, 26);
            this.tabOrdenesEstado.Name = "tabOrdenesEstado";
            this.tabOrdenesEstado.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrdenesEstado.Size = new System.Drawing.Size(742, 420);
            this.tabOrdenesEstado.TabIndex = 2;
            this.tabOrdenesEstado.Text = "Órdenes por Estado";
            this.tabOrdenesEstado.UseVisualStyleBackColor = true;
            // 
            // btnOrdenesEstado
            // 
            this.btnOrdenesEstado.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdenesEstado.Location = new System.Drawing.Point(10, 10);
            this.btnOrdenesEstado.Name = "btnOrdenesEstado";
            this.btnOrdenesEstado.Size = new System.Drawing.Size(150, 28);
            this.btnOrdenesEstado.TabIndex = 1;
            this.btnOrdenesEstado.Text = "Generar Reporte";
            this.btnOrdenesEstado.UseVisualStyleBackColor = true;
            this.btnOrdenesEstado.Click += new System.EventHandler(this.btnOrdenesEstado_Click);
            // 
            // dgvOrdenesEstado
            // 
            this.dgvOrdenesEstado.AllowUserToAddRows = false;
            this.dgvOrdenesEstado.AllowUserToDeleteRows = false;
            this.dgvOrdenesEstado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesEstado.Location = new System.Drawing.Point(10, 50);
            this.dgvOrdenesEstado.Name = "dgvOrdenesEstado";
            this.dgvOrdenesEstado.ReadOnly = true;
            this.dgvOrdenesEstado.Size = new System.Drawing.Size(720, 360);
            this.dgvOrdenesEstado.TabIndex = 0;
            // 
            // tabPorTipo
            // 
            this.tabPorTipo.Controls.Add(this.btnPorTipo);
            this.tabPorTipo.Controls.Add(this.dgvPorTipo);
            this.tabPorTipo.Location = new System.Drawing.Point(4, 26);
            this.tabPorTipo.Name = "tabPorTipo";
            this.tabPorTipo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPorTipo.Size = new System.Drawing.Size(742, 420);
            this.tabPorTipo.TabIndex = 3;
            this.tabPorTipo.Text = "Por Tipo de Mantenimiento";
            this.tabPorTipo.UseVisualStyleBackColor = true;
            // 
            // btnPorTipo
            // 
            this.btnPorTipo.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPorTipo.Location = new System.Drawing.Point(10, 10);
            this.btnPorTipo.Name = "btnPorTipo";
            this.btnPorTipo.Size = new System.Drawing.Size(150, 28);
            this.btnPorTipo.TabIndex = 1;
            this.btnPorTipo.Text = "Generar Reporte";
            this.btnPorTipo.UseVisualStyleBackColor = true;
            this.btnPorTipo.Click += new System.EventHandler(this.btnPorTipo_Click);
            // 
            // dgvPorTipo
            // 
            this.dgvPorTipo.AllowUserToAddRows = false;
            this.dgvPorTipo.AllowUserToDeleteRows = false;
            this.dgvPorTipo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPorTipo.Location = new System.Drawing.Point(10, 50);
            this.dgvPorTipo.Name = "dgvPorTipo";
            this.dgvPorTipo.ReadOnly = true;
            this.dgvPorTipo.Size = new System.Drawing.Size(720, 360);
            this.dgvPorTipo.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(612, 505);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 35);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 552);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes del Sistema";
            this.tabControl.ResumeLayout(false);
            this.tabHistorico.ResumeLayout(false);
            this.tabHistorico.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.tabResumenTecnico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenTecnico)).EndInit();
            this.tabOrdenesEstado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesEstado)).EndInit();
            this.tabPorTipo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorTipo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabHistorico;
        private System.Windows.Forms.TabPage tabResumenTecnico;
        private System.Windows.Forms.TabPage tabOrdenesEstado;
        private System.Windows.Forms.TabPage tabPorTipo;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.DataGridView dgvResumenTecnico;
        private System.Windows.Forms.DataGridView dgvOrdenesEstado;
        private System.Windows.Forms.DataGridView dgvPorTipo;
        private System.Windows.Forms.Button btnHistorico;
        private System.Windows.Forms.Label lblEquipoReporte;
        private System.Windows.Forms.ComboBox cmbEquipoReporte;
        private System.Windows.Forms.Button btnResumenTecnico;
        private System.Windows.Forms.Button btnOrdenesEstado;
        private System.Windows.Forms.Button btnPorTipo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblPromedioDias;
    }
}
