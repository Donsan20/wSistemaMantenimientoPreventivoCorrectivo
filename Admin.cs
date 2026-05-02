using System;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public partial class Admin : Form
    {
        private string _rolUsuario;

        // Sobrecarga del constructor para recibir el ROL
        public Admin(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;
            
            ConfigurarPermisos();

            // Suscribimos el botón de nueva orden
            button4.Click += AbrirNuevaOrden;
        }

        public Admin()
        {
            InitializeComponent();
        }

        private void ConfigurarPermisos()
        {
            // CONTROL DE ACCESO
            if (_rolUsuario == "tecnico")
            {
                button3.Visible = false; // EQUIPOS
                button2.Visible = false; // REPORTES
            }
            else if (_rolUsuario == "super")
            {
                button4.Visible = false; // NUEVA ORDEN
            }
            // Admin tiene todo los permisos
        }

        private void AbrirNuevaOrden(object sender, EventArgs e)
        {
            // Instanciamos y abrimos el formulario
            OrdenDeTrabajo frm = new OrdenDeTrabajo();
            frm.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
