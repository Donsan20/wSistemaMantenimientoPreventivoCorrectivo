using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Admin : Form
    {
        private string _rolUsuario;

        public Admin(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            // Llamamos aux para actualizar el filtro 
            AuxialiarInterfaz.ConfigurarFiltrosMantenimiento(cmbFiltrar);
            ConfigurarPermisos();

            btnNuevaOrden.Click += AbrirNuevaOrden;
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
                btnReportes.Visible = false;
            }
            else if (_rolUsuario == "Supervisor")
            {
                btnNuevaOrden.Visible = false;
            }
        }

        private void CargarTablaOrdenes()
        {
            
            // usamos  clase ConexionBD.
            string query = @"SELECT 
                                O.IdOrden AS 'Cod orden', 
                                E.NombreEquipo AS 'Equipo Afectado', 
                                O.TipoMantenimiento AS 'Tipo', 
                                O.FechaProgramada AS 'Fecha Prog', 
                                O.EstadoOrden AS 'Estado actual'
                             FROM OrdenesTrabajo O
                             INNER JOIN Equipos E ON O.IdEquipo = E.IdEquipo";

            // Usamos la clase experta
            dataGridView.DataSource = ConexionBD.CargarDatos(query);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarAlertas()
        {
            // actualizacion mantenimiente programados 
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT COUNT(*) 
                                     FROM OrdenesTrabajo 
                                     WHERE EstadoOrden = 'Pendiente' 
                                     AND FechaProgramada BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE())";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int mantenimientosProximos = Convert.ToInt32(command.ExecuteScalar());

                        if (mantenimientosProximos > 0)
                        {
                            pnlAlertas.BackColor = System.Drawing.Color.Tomato;
                            lblAlertas.Text = $"¡ALERTA! Hay {mantenimientosProximos} mantenimiento(s) programado(s) para los próximos 7 días.";
                        }
                        else
                        {
                            pnlAlertas.BackColor = System.Drawing.Color.MediumSeaGreen;
                            lblAlertas.Text = "Todo al día. No hay mantenimientos críticos próximos.";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblAlertas.Text = "Error al cargar alertas.";
                }
            }
        }

        private void AbrirNuevaOrden(object sender, EventArgs e)
        {
            OrdenDeTrabajo frm = new OrdenDeTrabajo();
            frm.ShowDialog();
            CargarTablaOrdenes();
            CargarAlertas();
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
                else if (seleccion == "Pendiente" || seleccion == "Completada")
                {
                    tablaEnMemoria.DefaultView.RowFilter = $"[Estado actual] = '{seleccion}'";
                }
            }
        }
    }
}