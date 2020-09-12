using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string aVideoPath = string.Concat(ConfigurationManager.AppSettings["devo-path"], "\\2.1.avi");
            string aVideoPath = @"C:\Users\sebastiangon11\source\repos\um - image - capture\resources\devo\1.2.avi";
            //string aVideoPath = "C:\\Users\\sebastiangon11\\source\\repos\\um - image - capture\\resources\\devo\\1.2.avi";
            try
            {
                WMPlayer.URL = aVideoPath;
                //WMPlayer.Ctlcontrols.play();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
