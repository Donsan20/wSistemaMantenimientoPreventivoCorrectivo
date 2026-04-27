using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data; // Necesario para usar DataTable


namespace wSistemaMantenimientoPreventivoCorrectivo
{
    // ¡Ojo acá! Cambiamos de UserControl a Form para que lo puedas abrir como ventana
    public partial class OrdenDeTrabajo : Form
    {
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
            //llenar el combo box con los tipos de mantenimiento 
            cmbTipo.Items.Add("Correctivo");
            cmbTipo.Items.Add("Preventivo");
            cmbTipo.SelectedIndex = 0;   //selecciona el primero por defecto
            dtpFecha.MinDate = DateTime.Today;    // Un mantenimiento programado no debería tener una fecha en el pasado

            // 2. Cargar los equipos desde SQL Server
            string connectionString = @"Server=DESKTOP-8T64Q5F\SQLEXPRESS;Database=MantenimientoDB;Integrated Security=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Solo queremos mostrar los equipos que estén 'Activos'
                    string query = "SELECT IdEquipo, NombreEquipo FROM Equipos WHERE Estado = 'Activo'";

                    // El Adapter es el puente que trae los datos y llena el DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dtEquipos = new DataTable();
                    adapter.Fill(dtEquipos);

                    // 3. Configurar el ComboBox para que entienda el DataTable
                    cmbEquipo.DataSource = dtEquipos;

                    // DisplayMember: Lo que el técnico VA A LEER (Ej: "Motor Eléctrico Principal")
                    cmbEquipo.DisplayMember = "NombreEquipo";

                    // ValueMember: Lo que el sistema VA A GUARDAR (El ID oculto, Ej: 1)
                    cmbEquipo.ValueMember = "IdEquipo";

                    cmbEquipo.SelectedIndex = -1; // Dejarlo en blanco por defecto
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al cargar los equipos: " + ex.Message);
                }
            }

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (cmbEquipo.SelectedIndex == -1)
            {
                //seleccionar un equipo
                MessageBox.Show("Selecciona un equipo", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEquipo.Focus(); // el cursor queda en el espacio que falta
                return;
            }

            //validar descripciom
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripcion del mantenimiento es obligatoria", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            // 2. RECOLECTAR LOS DATOS DE LA INTERFAZ
            // Aquí usamos SelectedValue para sacar el Id numérico del equipo seleccionado
            int idEquipo = Convert.ToInt32(cmbEquipo.SelectedValue);
            string tipoMantenimiento = cmbTipo.SelectedItem.ToString();
            DateTime fechaProgramada = dtpFecha.Value;
            string descripcion = txtDescripcion.Text.Trim();

            // OJO: Id temporal para que la base de datos nos deje guardar.
            int idTecnicoTemporal = 3;

            // 3. CONECTAR Y GUARDAR
            string connectionString = @"Server=DESKTOP-8T64Q5F\SQLEXPRESS;Database=MantenimientoDB;Integrated Security=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // La consulta INSERT. Los @ indican que son variables que pasaremos después
                    string query = @"INSERT INTO OrdenesTrabajo 
                             (IdEquipo, IdTecnico, TipoMantenimiento, FechaProgramada, DescripcionFalla) 
                             VALUES (@Equipo, @Tecnico, @Tipo, @Fecha, @Desc)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Pasamos los valores reales a los parámetros para evitar inyección SQL
                        command.Parameters.AddWithValue("@Equipo", idEquipo);
                        command.Parameters.AddWithValue("@Tecnico", idTecnicoTemporal);
                        command.Parameters.AddWithValue("@Tipo", tipoMantenimiento);
                        command.Parameters.AddWithValue("@Fecha", fechaProgramada);
                        command.Parameters.AddWithValue("@Desc", descripcion);

                        // ExecuteNonQuery se usa cuando NO esperamos que la base de datos nos devuelva una tabla
                        // (como en un SELECT), sino que solo queremos que ejecute la acción (INSERT/UPDATE/DELETE).
                        // Devuelve el número de filas afectadas.
                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Orden generada correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Cierra la ventana emergente
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al guardar en la base de datos: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
