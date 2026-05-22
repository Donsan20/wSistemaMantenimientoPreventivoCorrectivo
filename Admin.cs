using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Admin : Form, ISuscriptor
    {
        private string _rolUsuario;
        private CN_Ordenes objOrden = new CN_Ordenes();

        public Admin(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            // Suscribirse al patrón Observer para refrescar automáticamente
            GestorEventos.Suscribirse(this);

            // Llamamos aux para actualizar el filtro 
            AuxialiarInterfaz.ConfigurarFiltrosMantenimiento(cmbFiltrar);
            ConfigurarPermisos();

            // Conectar todos los botones del menú lateral
            btnNuevaOrden.Click += AbrirNuevaOrden;
            btnEquipos.Click += AbrirGestionEquipos;
            btnUsuarios.Click += AbrirGestionUsuarios;
            btnReportes.Click += AbrirReportes;
            lblRolSesion.Text = "Sesión activa: " + _rolUsuario.ToUpper();

            CargarTablaOrdenes();
            CargarAlertas();
        }

        public Admin()
        {
            InitializeComponent();
        }

        private void ConfigurarPermisos()
        {
            if (_rolUsuario == "Tecnico")
            {
                btnEquipos.Visible = false;
                btnUsuarios.Visible = false;
                btnReportes.Visible = false;
            }
            else if (_rolUsuario == "Supervisor" || _rolUsuario == "Super")
            {
                btnNuevaOrden.Visible = false;
            }
        }

        private void CargarTablaOrdenes()
        {
            try
            {
                // Usamos la capa de negocio
                dataGridView.DataSource = objOrden.ListarOrdenes();
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAlertas()
        {
            try
            {
                // Llamamos a la capa de negocio
                int mantenimientosProximos = objOrden.ContarAlertasProximas();

                if (mantenimientosProximos > 0)
                {
                    pnlAlertas.BackColor = Color.Tomato;
                    lblAlertas.Text = $"¡ALERTA! Hay {mantenimientosProximos} mantenimiento(s) programado(s) para los próximos 7 días.";
                }
                else
                {
                    pnlAlertas.BackColor = Color.MediumSeaGreen;
                    lblAlertas.Text = "Todo al día. No hay mantenimientos críticos próximos.";
                }
            }
            catch (Exception ex)
            {
                lblAlertas.Text = "Error al cargar alertas: " + ex.Message;
            }
        }

        private void AbrirNuevaOrden(object sender, EventArgs e)
        {
            OrdenDeTrabajo frm = new OrdenDeTrabajo();
            frm.ShowDialog();
            CargarTablaOrdenes();
            CargarAlertas();
        }

        private void AbrirGestionEquipos(object sender, EventArgs e)
        {
            GestionEquipos frm = new GestionEquipos();
            frm.ShowDialog();
            CargarTablaOrdenes();
        }

        private void AbrirGestionUsuarios(object sender, EventArgs e)
        {
            GestionUsuarios frm = new GestionUsuarios();
            frm.ShowDialog();
        }

        private void AbrirReportes(object sender, EventArgs e)
        {
            Reportes frm = new Reportes();
            frm.ShowDialog();
        }

        // Filtro para estado de las OT
        private void cmbFiltrar_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            DataTable tablaEnMemoria = dataGridView.DataSource as DataTable;

            if (tablaEnMemoria != null)
            {
                string seleccion = cmbFiltrar.SelectedItem.ToString();

                if (seleccion == "Todos")
                {
                    tablaEnMemoria.DefaultView.RowFilter = "";
                }
                else if (seleccion == "Preventivo" || seleccion == "Correctivo")
                {
                    tablaEnMemoria.DefaultView.RowFilter = $"Tipo = '{seleccion}'";
                }
                else if (seleccion == "Pendiente" || seleccion == "En Proceso" || seleccion == "Completada" || seleccion == "Cancelada")
                {
                    tablaEnMemoria.DefaultView.RowFilter = $"[Estado actual] = '{seleccion}'";
                }
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Validamos con el usuario si realmente desea salir (muy importante)
            DialogResult confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas cerrar sesión y volver al inicio?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Si el usuario hace clic en "Sí"
            if (confirmacion == DialogResult.Yes)
            {
                // Desuscribirse del Observer antes de cerrar
                GestorEventos.Desuscribirse(this);
                this.Close();
            }
        }

        // ==================== Patrón Observer ====================

        /// <summary>
        /// Implementación de ISuscriptor: se ejecuta cuando otro formulario notifica un cambio.
        /// </summary>
        public void Actualizar(string tipoEvento, object datos = null)
        {
            // Refrescar la tabla de órdenes cuando hay cambios relevantes
            if (tipoEvento == "OrdenCreada" || tipoEvento == "OrdenActualizada" || tipoEvento == "OrdenEliminada")
            {
                // Usar Invoke para asegurar que la actualización sea en el hilo de la UI
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => {
                        CargarTablaOrdenes();
                        CargarAlertas();
                    }));
                }
                else
                {
                    CargarTablaOrdenes();
                    CargarAlertas();
                }
            }
        }
    }
}
