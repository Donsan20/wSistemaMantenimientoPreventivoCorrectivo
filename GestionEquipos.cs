using System;
using System.Data;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class GestionEquipos : Form
    {
        private CN_Equipos objNegocio = new CN_Equipos();
        private bool _modoEdicion = false;
        private int _idEquipoSeleccionado = 0;

        public GestionEquipos()
        {
            InitializeComponent();
            CargarEquipos();
        }

        /// <summary>
        /// Carga todos los equipos en el DataGridView.
        /// </summary>
        private void CargarEquipos()
        {
            try
            {
                dgvEquipos.DataSource = objNegocio.ListarTodosEquipos();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Configura las columnas del DataGridView para mejor lectura.
        /// </summary>
        private void ConfigurarGrilla()
        {
            dgvEquipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEquipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipos.ReadOnly = true;
            dgvEquipos.AllowUserToAddRows = false;
            dgvEquipos.AllowUserToDeleteRows = false;

            // Ocultar ID de la vista (se usa internamente)
            if (dgvEquipos.Columns["IdEquipo"] != null)
            {
                dgvEquipos.Columns["IdEquipo"].Visible = false;
            }
        }

        /// <summary>
        /// Limpia los campos del formulario para un nuevo registro.
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            cmbEstado.SelectedIndex = 0;
            _modoEdicion = false;
            _idEquipoSeleccionado = 0;
            btnGuardar.Text = "Guardar";
            txtNombre.Focus();
        }

        // ==================== EVENTOS ====================

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string estado = cmbEstado.SelectedItem.ToString();

                if (_modoEdicion)
                {
                    // Modo edición
                    bool exito = objNegocio.ActualizarEquipo(_idEquipoSeleccionado, nombre, estado);
                    if (exito)
                    {
                        MessageBox.Show("Equipo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Modo nuevo
                    bool exito = objNegocio.InsertarEquipo(nombre, estado);
                    if (exito)
                    {
                        MessageBox.Show("Equipo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CargarEquipos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idEquipoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un equipo de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de que desea eliminar este equipo?\nEsta acción es irreversible.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    bool exito = objNegocio.EliminarEquipo(_idEquipoSeleccionado);
                    if (exito)
                    {
                        MessageBox.Show("Equipo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEquipos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string busqueda = txtBusqueda.Text.Trim();

                if (string.IsNullOrEmpty(busqueda))
                {
                    CargarEquipos();
                    return;
                }

                dgvEquipos.DataSource = objNegocio.BuscarEquipoPorNombre(busqueda);
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            // Búsqueda en tiempo real
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                CargarEquipos();
            }
            else
            {
                try
                {
                    dgvEquipos.DataSource = objNegocio.BuscarEquipoPorNombre(txtBusqueda.Text.Trim());
                    ConfigurarGrilla();
                }
                catch
                {
                    // Silenciar errores mientras escribe
                }
            }
        }

        private void dgvEquipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Solo responder a clicks válidos en filas de datos
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvEquipos.Rows[e.RowIndex];

            _idEquipoSeleccionado = Convert.ToInt32(fila.Cells["IdEquipo"].Value);
            txtNombre.Text = fila.Cells["NombreEquipo"].Value.ToString();
            cmbEstado.SelectedItem = fila.Cells["Estado"].Value.ToString();

            _modoEdicion = true;
            btnGuardar.Text = "Actualizar";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
