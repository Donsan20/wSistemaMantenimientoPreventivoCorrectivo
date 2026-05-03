using System;
using System.Windows.Forms;
using wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Form1 : Form
    {
        private CN_Usuarios objUsuario = new CN_Usuarios();

        public Form1()
        {
            InitializeComponent();
            btnIngresar.Click += btnIngresar_Click;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUsuario.Text.Trim().ToLower();
                string pass = txtContraseña.Text.Trim();

                // Llamamos a la Capa de Negocio, que a su vez llama a la Capa de Datos
                string rolAsignado = objUsuario.ValidarLogin(user, pass);

                if (!string.IsNullOrEmpty(rolAsignado))
                {
                    // ¡Magia! Abrimos el Dashboard y le pasamos el rol real
                    Admin ventanaDashboard = new Admin(rolAsignado);
                    this.Hide();
                    ventanaDashboard.ShowDialog();
                    
                    // Cuando el usuario cierra sesión (o cierra la ventana), el código continúa aquí.
                    // Limpiamos los campos de forma natural y volvemos a mostrar el login:
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    txtUsuario.Focus(); // Ponemos el cursor en el usuario
                    this.Show();
                }
                else
                {
                    // Si no encuentra nada, los datos están mal
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
