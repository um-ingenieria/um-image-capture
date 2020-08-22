using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCapturaDePantalla
{
    public partial class ImageDisplay : Form
    {
        string path = "./";
        public ImageDisplay(string path)
        {
            InitializeComponent();
            this.path = path;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void ChangeImage(string iaps_id)
        {
            SetPictureLocation(iaps_id);
        }

        private void SetPictureLocation(string fileName)
        {
            pictureBox.ImageLocation = string.Concat(path, "/", fileName);
        }
    }
}
