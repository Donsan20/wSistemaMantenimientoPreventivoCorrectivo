using System;
using System.Data;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class GestionUsuarios : Form
    {
        private CN_Usuarios objNegocio = new CN_Usuarios();
        private bool _modoEdicion = false;
        private int _idUsuarioSeleccionado = 0;

        public GestionUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
            CargarRoles();
        }

        /// <summary>
        /// Carga todos los usuarios en el DataGridView.
        /// </summary>
        private void CargarUsuarios()
        {
            try
            {
                dgvUsuarios.DataSource = objNegocio.ListarUsuarios();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga los roles disponibles en el ComboBox.
        /// </summary>
        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("1|Admin");
            cmbRol.Items.Add("2|Tecnico");
            cmbRol.Items.Add("3|Super");
            cmbRol.SelectedIndex = 0;
        }

        /// <summary>
        /// Configura las columnas del DataGridView para mejor lectura.
        /// </summary>
        private void ConfigurarGrilla()
        {
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;

            // Ocultar ID de la vista
            if (dgvUsuarios.Columns["IdUsuario"] != null)
            {
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
            }

            // Formatear columna Estado
            if (dgvUsuarios.Columns["Estado"] != null)
            {
                dgvUsuarios.Columns["Estado"].DefaultCellStyle.Format = "True/False";
            }
        }

        /// <summary>
        /// Obtiene el ID del rol seleccionado en el ComboBox.
        /// </summary>
        private int ObtenerIdRolSeleccionado()
        {
            string seleccion = cmbRol.SelectedItem.ToString();
            return int.Parse(seleccion.Split('|')[0]);
        }

        /// <summary>
        /// Selecciona el rol en el ComboBox según su ID.
        /// </summary>
        private void SeleccionarRolPorId(int idRol)
        {
            foreach (string item in cmbRol.Items)
            {
                if (item.StartsWith(idRol + "|"))
                {
                    cmbRol.SelectedItem = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Limpia los campos del formulario.
        /// </summary>
        private void LimpiarCampos()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbEstado.SelectedIndex = 0;
            cmbRol.SelectedIndex = 0;
            _modoEdicion = false;
            _idUsuarioSeleccionado = 0;
            btnGuardar.Text = "Guardar";
            txtUsername.Focus();
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
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                bool estado = cmbEstado.SelectedItem.ToString() == "Activo";
                int idRol = ObtenerIdRolSeleccionado();

                if (_modoEdicion)
                {
                    bool exito = objNegocio.ActualizarUsuario(_idUsuarioSeleccionado, username, password, estado, idRol);
                    if (exito)
                    {
                        MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    bool exito = objNegocio.InsertarUsuario(username, password, estado, idRol);
                    if (exito)
                    {
                        MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de que desea eliminar este usuario?\nEsta acción desactivará su acceso al sistema.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    bool exito = objNegocio.EliminarUsuario(_idUsuarioSeleccionado);
                    if (exito)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
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
                    CargarUsuarios();
                    return;
                }

                dgvUsuarios.DataSource = objNegocio.BuscarUsuarioPorNombre(busqueda);
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                CargarUsuarios();
            }
            else
            {
                try
                {
                    dgvUsuarios.DataSource = objNegocio.BuscarUsuarioPorNombre(txtBusqueda.Text.Trim());
                    ConfigurarGrilla();
                }
                catch
                {
                    // Silenciar errores mientras escribe
                }
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

            _idUsuarioSeleccionado = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
            txtUsername.Text = fila.Cells["Username"].Value.ToString();
            txtPassword.Text = ""; // No mostramos la contraseña por seguridad
            cmbEstado.SelectedItem = Convert.ToBoolean(fila.Cells["Estado"].Value) ? "Activo" : "Inactivo";

            // Mapear el rol
            string rolNombre = fila.Cells["Rol"].Value.ToString();
            switch (rolNombre)
            {
                case "Admin":
                    SeleccionarRolPorId(1);
                    break;
                case "Tecnico":
                    SeleccionarRolPorId(2);
                    break;
                case "Super":
                    SeleccionarRolPorId(3);
                    break;
            }

            _modoEdicion = true;
            btnGuardar.Text = "Actualizar";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
