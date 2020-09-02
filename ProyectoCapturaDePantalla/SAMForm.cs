using ProyectoCapturaDePantalla.Domain.SAM;
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
    public partial class SAMForm : Form
    {

        int arousal = 0;
        int valence = 0;

        public SAMForm()
        {   
            InitializeComponent();
            trackBarArousal.Value = arousal;
            trackBarValence.Value = valence;
        }

        public SAM getSAMResponse()
        {
            return new SAM(arousal, valence);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBarValence_Scroll(object sender, EventArgs e)
        {
            valence = int.Parse(trackBarValence.Value.ToString());
        }

        private void trackBarArousal_Scroll(object sender, EventArgs e)
        {
            arousal = int.Parse(trackBarArousal.Value.ToString());
        }
    }
}
