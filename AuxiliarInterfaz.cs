using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public static class AuxialiarInterfaz
    {
        // Método público y estático que recibe cualquier ComboBox y lo llena
        public static void ConfigurarFiltrosMantenimiento(ComboBox combo)
        {
            combo.Items.Clear();
            combo.Items.AddRange(new string[] { "Todos", "Preventivo", "Correctivo", "Pendiente", "Completada" });
            combo.SelectedIndex = 0;
        }
    }
}
