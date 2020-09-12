using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCapturaDePantalla
{
    public partial class VideoDisplay : Form
    {
        public VideoDisplay()
        {
            InitializeComponent();
            FileInfo file = new FileInfo(@"C:\Users\sebastiangon11\source\repos\um - image - capture\resources\devo\1.2.avi");
            vlcPlayer.SetMedia(file);
            vlcPlayer.Play();
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            if (IntPtr.Size == 4)
            {
                //e.VlcLibDirectory = new DirectoryInfo(@"..\..\..\lib\x86");
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(".", "libvlc", "win-x86"));
            }
            else
            {
                //e.VlcLibDirectory = new DirectoryInfo(@"..\..\..\lib\x64\");
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(".", "libvlc", "win-x64"));
            }
        }
    }
}
