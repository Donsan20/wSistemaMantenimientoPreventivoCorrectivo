using System;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // Suscribimos el botón para que ejecute el login al hacer clic
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim().ToLower();
            string pass = textBox2.Text.Trim();

            // Validación de credenciales
            if (pass == "123")
            {
                if (user == "admin" || user == "tecnico" || user == "super")
                {
                    // Instanciamos el Dashboard y le pasamos el ROL
                    Admin dashboard = new Admin(user);
                    
                    this.Hide(); // Ocultar el Login
                    dashboard.ShowDialog(); // Mostrar el Dashboard bloqueando
                    this.Close(); // Cuando se cierre el Dashboard, cerramos el programa
                    return;
                }
            }

            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}
