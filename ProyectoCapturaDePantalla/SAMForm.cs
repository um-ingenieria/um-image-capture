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

        int? arousal = null;
        int? valence = null;

        public SAMForm()
        {   
            InitializeComponent();
        }

        public SAM getSAMResponse()
        {
            return new SAM((int)arousal, (int)valence);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBarValence_Scroll(object sender, EventArgs e)
        {
            valence = int.Parse(trackBarValence.Value.ToString());
            if (arousal != null)
            {
                btnAccept.Enabled = true;
            }
        }

        private void trackBarArousal_Scroll(object sender, EventArgs e)
        {
            arousal = int.Parse(trackBarArousal.Value.ToString());
            if (valence != null)
            {
                btnAccept.Enabled = true;
            }
        }
    }
}
