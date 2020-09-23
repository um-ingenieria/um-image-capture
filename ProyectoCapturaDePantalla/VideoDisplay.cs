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
        string path = "./";
        public event EventHandler onPlaylistEnd;
        List<DEVO> videoList; 
        int actual = 0;

        public VideoDisplay(string path, List<DEVO> videoList)
        {
            InitializeComponent();
            this.path = path;
            this.videoList = videoList;
            startPresentation();

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

        private void SetVideo(string fileName)
        {
            //vlcPlayer.Stop();
            FileInfo file = new FileInfo(@"..\..\..\resources\devo\" + fileName);
            vlcPlayer.ResetMedia();
            vlcPlayer.SetMedia(file);
            vlcPlayer.Play();
        }

        private void vlcPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            if(actual > videoList.Count)
            {
                this.Close();
            }
        }

        internal void startPresentation()
        {
            if(videoList.Count > 0)
            {
                this.SetVideo(string.Concat(videoList[actual].Id, ".mp4"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actual++;
            if (actual < videoList.Count)
            {
                this.SetVideo(string.Concat(videoList[actual].Id, ".mp4"));
            } else
            {
                this.Close();
            }
        }

        private void VideoDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
