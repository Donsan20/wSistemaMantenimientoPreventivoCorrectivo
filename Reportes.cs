using System;
using System.Data;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Reportes : Form
    {
        private CN_Reportes objReportes = new CN_Reportes();
        private CN_Equipos objEquipos = new CN_Equipos();

        public Reportes()
        {
            InitializeComponent();
            CargarEquipos();
        }

        private void CargarEquipos()
        {
            try
            {
                cmbEquipoReporte.DataSource = objEquipos.ListarEquiposActivos();
                cmbEquipoReporte.DisplayMember = "NombreEquipo";
                cmbEquipoReporte.ValueMember = "IdEquipo";
                cmbEquipoReporte.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar equipos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
        }

        // ==================== PESTAÑA 1: Histórico por Equipo ====================

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            if (cmbEquipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un equipo para ver su histórico.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idEquipo = Convert.ToInt32(cmbEquipoReporte.SelectedValue);
                dgvHistorico.DataSource = objReportes.ReporteHistoricoPorEquipo(idEquipo);
                ConfigurarGrilla(dgvHistorico);

                // Calcular y mostrar tiempo promedio
                double promedio = objReportes.TiempoPromedioEntreMantenimientos(idEquipo);
                if (promedio > 0)
                {
                    lblPromedioDias.Text = $"Tiempo promedio entre mantenimientos: {promedio:F1} días";
                    lblPromedioDias.ForeColor = System.Drawing.Color.DarkGreen;
                }
                else
                {
                    lblPromedioDias.Text = "No hay suficientes datos para calcular el promedio.";
                    lblPromedioDias.ForeColor = System.Drawing.Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== PESTAÑA 2: Resumen por Técnico ====================

        private void btnResumenTecnico_Click(object sender, EventArgs e)
        {
            try
            {
                dgvResumenTecnico.DataSource = objReportes.ReporteResumenPorTecnico();
                ConfigurarGrilla(dgvResumenTecnico);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== PESTAÑA 3: Órdenes por Estado ====================

        private void btnOrdenesEstado_Click(object sender, EventArgs e)
        {
            try
            {
                dgvOrdenesEstado.DataSource = objReportes.ReporteOrdenesPorEstado();
                ConfigurarGrilla(dgvOrdenesEstado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== PESTAÑA 4: Por Tipo de Mantenimiento ====================

        private void btnPorTipo_Click(object sender, EventArgs e)
        {
            try
            {
                dgvPorTipo.DataSource = objReportes.ReportePorTipoMantenimiento();
                ConfigurarGrilla(dgvPorTipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
