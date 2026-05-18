using System;
using System.Data;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class OrdenDeTrabajo : Form
    {
        private CN_Equipos objEquipo = new CN_Equipos();
        private CN_Ordenes objOrden = new CN_Ordenes();
        private bool _modoEdicion = false;
        private int _idOrdenSeleccionada = 0;

        public OrdenDeTrabajo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void OrdenDeTrabajo_Load(object sender, EventArgs e)
        {
            // Configurar ComboBox de tipos
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Correctivo");
            cmbTipo.Items.Add("Preventivo");
            cmbTipo.SelectedIndex = 0;

            // Configurar ComboBox de estados
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("En Proceso");
            cmbEstado.Items.Add("Completada");
            cmbEstado.Items.Add("Cancelada");
            cmbEstado.SelectedIndex = 0;

            // Configurar fecha mínima
            dtpFecha.MinDate = DateTime.Today;

            // Cargar equipos activos
            CargarEquipos();

            // Cargar órdenes existentes
            CargarOrdenes();

            // Iniciar en modo creación
            _modoEdicion = false;
            btnGuardar.Text = "Guardar";
        }

        private void CargarEquipos()
        {
            try
            {
                cmbEquipo.DataSource = objEquipo.ListarEquiposActivos();
                cmbEquipo.DisplayMember = "NombreEquipo";
                cmbEquipo.ValueMember = "IdEquipo";
                cmbEquipo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar equipos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarOrdenes()
        {
            try
            {
                dgvOrdenes.DataSource = objOrden.ListarOrdenes();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.ReadOnly = true;
            dgvOrdenes.AllowUserToAddRows = false;
            dgvOrdenes.AllowUserToDeleteRows = false;

            // Ocultar ID interno
            if (dgvOrdenes.Columns["Cod orden"] != null)
            {
                dgvOrdenes.Columns["Cod orden"].Visible = false;
            }
        }

        private void LimpiarCampos()
        {
            cmbEquipo.SelectedIndex = -1;
            cmbTipo.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Today;
            txtDescripcion.Clear();
            cmbEstado.SelectedIndex = 0;
            _modoEdicion = false;
            _idOrdenSeleccionada = 0;
            btnGuardar.Text = "Guardar";
            cmbEquipo.Focus();
        }

        // ==================== EVENTOS ====================

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones UI
                if (cmbEquipo.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecciona un equipo", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbEquipo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text.Trim()))
                {
                    MessageBox.Show("Ingresa una descripción de la falla", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescripcion.Focus();
                    return;
                }

                int idEquipo = Convert.ToInt32(cmbEquipo.SelectedValue);
                string tipoMantenimiento = cmbTipo.SelectedItem.ToString();
                DateTime fechaProgramada = dtpFecha.Value;
                string descripcion = txtDescripcion.Text.Trim();
                int idTecnico = 2; // Simulación del usuario logueado

                bool exito = false;

                    if (_modoEdicion)
                    {
                        // Modo edición
                        string estadoOrden = cmbEstado.SelectedItem.ToString();
                        exito = objOrden.ActualizarOrden(_idOrdenSeleccionada, idEquipo, idTecnico, tipoMantenimiento, fechaProgramada, descripcion, estadoOrden);
                        if (exito)
                        {
                            MessageBox.Show("Orden actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GestorEventos.NotificarOrdenActualizada(_idOrdenSeleccionada);
                        }
                    }
                    else
                    {
                        // Modo nuevo
                        exito = objOrden.InsertarOrden(idEquipo, idTecnico, tipoMantenimiento, fechaProgramada, descripcion);
                        if (exito)
                        {
                            MessageBox.Show("Orden generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GestorEventos.NotificarNuevaOrden();
                        }
                    }

                CargarOrdenes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (_idOrdenSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una orden de la lista para cambiar su estado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nuevoEstado = cmbEstado.SelectedItem.ToString();
                bool exito = objOrden.ActualizarEstadoOrden(_idOrdenSeleccionada, nuevoEstado);

                if (exito)
                {
                    MessageBox.Show($"Estado de la orden cambiado a: {nuevoEstado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GestorEventos.NotificarOrdenActualizada(_idOrdenSeleccionada);
                    CargarOrdenes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idOrdenSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una orden de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de que desea eliminar esta orden?\nSolo se pueden eliminar órdenes en estado Pendiente o Cancelada.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    bool exito = objOrden.EliminarOrden(_idOrdenSeleccionada);
                    if (exito)
                    {
                        MessageBox.Show("Orden eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GestorEventos.NotificarOrdenEliminada(_idOrdenSeleccionada);
                        CargarOrdenes();
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
                string equipo = txtBusquedaEquipo.Text.Trim();
                string tipo = cmbFiltroTipo.SelectedItem?.ToString();
                string estado = cmbFiltroEstado.SelectedItem?.ToString();

                // Si todos los filtros están vacíos, cargar todo
                if (string.IsNullOrEmpty(equipo) && (tipo == null || tipo == "Todos") && (estado == null || estado == "Todos"))
                {
                    CargarOrdenes();
                    return;
                }

                dgvOrdenes.DataSource = objOrden.BuscarOrdenes(
                    equipo,
                    (tipo == "Todos" ? null : tipo),
                    (estado == "Todos" ? null : estado),
                    null, null
                );
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBusquedaEquipo.Clear();
            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroEstado.SelectedIndex = 0;
            CargarOrdenes();
        }

        private void dgvOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvOrdenes.Rows[e.RowIndex];

            // Obtener el ID de la orden
            _idOrdenSeleccionada = Convert.ToInt32(fila.Cells["Cod orden"].Value);

            // Cargar datos en el formulario
            string equipoNombre = fila.Cells["Equipo Afectado"].Value.ToString();
            string tipo = fila.Cells["Tipo"].Value.ToString();
            string estado = fila.Cells["Estado actual"].Value.ToString();

            // Seleccionar equipo en el combo
            for (int i = 0; i < cmbEquipo.Items.Count; i++)
            {
                DataRowView item = cmbEquipo.Items[i] as DataRowView;
                if (item != null && item["NombreEquipo"].ToString() == equipoNombre)
                {
                    cmbEquipo.SelectedIndex = i;
                    break;
                }
            }

            // Seleccionar tipo
            cmbTipo.SelectedItem = tipo;

            // Seleccionar estado
            cmbEstado.SelectedItem = estado;

            // Obtener descripción completa desde la capa de datos
            try
            {
                DataTable detalle = objOrden.ObtenerDetalleOrden(_idOrdenSeleccionada);
                if (detalle.Rows.Count > 0)
                {
                    txtDescripcion.Text = detalle.Rows[0]["Descripción"].ToString();

                    // Si hay fecha en el detalle, actualizar el DatePicker
                    if (detalle.Rows[0]["Fecha Programada"] != DBNull.Value)
                    {
                        dtpFecha.Value = Convert.ToDateTime(detalle.Rows[0]["Fecha Programada"]);
                    }
                }
            }
            catch
            {
                // Si no se puede obtener el detalle, dejar la descripción vacía
            }

            _modoEdicion = true;
            btnGuardar.Text = "Actualizar";
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
