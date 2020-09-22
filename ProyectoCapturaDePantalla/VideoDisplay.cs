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
        string path = "./";
        public event EventHandler onVideoEnd;

        public VideoDisplay(string path)
        {
            InitializeComponent();
            this.path = path;
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

        public void ChangeVideo(string id)
        {
            SetVideo(id);
        }

        private void SetVideo(string fileName)
        {
            vlcPlayer.Stop();
            FileInfo file = new FileInfo(@"..\..\..\resources\devo\" + fileName);
            vlcPlayer.SetMedia(file);
            vlcPlayer.Play();
            
        }

        private void vlcPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            onVideoEnd?.Invoke(this, null);
        }
    }
}
