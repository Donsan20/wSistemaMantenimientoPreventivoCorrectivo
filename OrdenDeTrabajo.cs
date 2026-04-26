using System;
using System.Windows.Forms;

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
            button1.Click += (s, e) => this.Close();
            
            // Configurar botón Guardar
            button2.Click += (s, e) => {
                MessageBox.Show("Orden generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
        }
    }
}
