using System;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class OrdenDeTrabajo : Form
    {
        private CN_Equipos objEquipo = new CN_Equipos();
        private CN_Ordenes objOrden = new CN_Ordenes();

        public OrdenDeTrabajo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Configurar botón Cancelar
            btncancelar.Click += (s, e) => this.Close();
        }

        private void OrdenDeTrabajo_Load(object sender, EventArgs e)
        {
            // Llenar el combo box con los tipos de mantenimiento 
            cmbTipo.Items.Add("Correctivo");
            cmbTipo.Items.Add("Preventivo");
            cmbTipo.SelectedIndex = 0;   // Selecciona el primero por defecto
            dtpFecha.MinDate = DateTime.Today; // Regla visual inicial

            // Cargar los equipos usando la capa de negocio
            try
            {
                cmbEquipo.DataSource = objEquipo.ListarEquiposActivos();
                cmbEquipo.DisplayMember = "NombreEquipo";
                cmbEquipo.ValueMember = "IdEquipo";
                cmbEquipo.SelectedIndex = -1; // Dejarlo en blanco por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar equipos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas de la UI
            if (cmbEquipo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un equipo", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEquipo.Focus(); 
                return;
            }

            int idEquipo = Convert.ToInt32(cmbEquipo.SelectedValue);
            string tipoMantenimiento = cmbTipo.SelectedItem.ToString();
            DateTime fechaProgramada = dtpFecha.Value;
            string descripcion = txtDescripcion.Text.Trim();
            
            // Simulación del usuario que está logueado
            int idTecnicoTemporal = 2;

            // Llamamos a la capa de negocio
            try
            {
                bool exito = objOrden.InsertarOrden(idEquipo, idTecnicoTemporal, tipoMantenimiento, fechaProgramada, descripcion);
                
                if (exito)
                {
                    MessageBox.Show("Orden generada correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Cierra la ventana emergente
                }
            }
            catch (Exception ex)
            {
                // Captura las validaciones de negocio o errores SQL
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
