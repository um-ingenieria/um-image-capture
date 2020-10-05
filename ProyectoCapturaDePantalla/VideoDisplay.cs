using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
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
        public event EventHandler onVideoEnd;

        public string Path;

        public VideoDisplay(string path)
        {
            InitializeComponent();
            this.Path = path;
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            if (IntPtr.Size == 4)
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(".", "libvlc", "win-x86"));
            else
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(".", "libvlc", "win-x64"));
        }

        public void play(string fileName)
        {
            FileInfo file = new FileInfo(this.Path + "\\" + fileName);
            vlcPlayer.ResetMedia();
            vlcPlayer.SetMedia(file);
            vlcPlayer.Play();
        }

        private void vlcPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            //this.onVideoEnd?.Invoke(this, e);
            // this.Close();
            //this.Invoke(new MethodInvoker(delegate { this.Close(); }));
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(this.Close));
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
