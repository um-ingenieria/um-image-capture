using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCapturaDePantalla
{
    public partial class Pulsador : Form
    {

        int valorExcitacion = 99;
        int valorValencia = 99;

        public Pulsador()
        {   
            InitializeComponent();
        }


        public int DarValorExcitacion()
        {
            return valorExcitacion;
        }

        public int DarValorValencia()
        {
            return valorValencia;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBarValence_Scroll(object sender, EventArgs e)
        {
            valorValencia = int.Parse(trackBarValence.Value.ToString());
        }

        private void trackBarArousal_Scroll(object sender, EventArgs e)
        {
            valorExcitacion = int.Parse(trackBarArousal.Value.ToString());
        }
    }
}
