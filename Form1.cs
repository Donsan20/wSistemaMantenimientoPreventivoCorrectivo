using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // Suscribimos el botón para que ejecute el login al hacer clic
            btnIngresar.Click += btnIngresar_Click;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim().ToLower();
            string pass = txtContraseña.Text.Trim();
            //conexion con mi base de datos sql server
            string connectionString = @"Server=DESKTOP-8T64Q5F\SQLEXPRESS;Database=MantenimientoDB;Integrated Security=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); //abrir conexion con la abse de datos

                    // 3. Creamos la consulta SQL (Usamos INNER JOIN para traer el nombre del rol)
                    string query = @"SELECT r.NombreRol 
                             FROM Usuarios u 
                             INNER JOIN Roles r ON u.IdRol = r.IdRol 
                             WHERE u.Username = @Usuario AND u.Password = @Password AND u.Estado = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 4. Usamos parámetros para evitar Inyección SQL (Seguridad)
                        command.Parameters.AddWithValue("@Usuario", user);
                        command.Parameters.AddWithValue("@Password", pass);

                        // 5. Ejecutamos el comando y leemos la respuesta
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro...
                            {
                                // Atrapamos el rol que nos devolvió la base de datos
                                string rolAsignado = reader["NombreRol"].ToString();

                                // ¡Magia! Abrimos el Dashboard y le pasamos el rol real
                                Admin dashboard = new Admin(rolAsignado);
                                this.Hide();
                                dashboard.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                // Si no encuentra nada, los datos están mal
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al cponectar la base de datos: " + ex.Message, "Errod de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
