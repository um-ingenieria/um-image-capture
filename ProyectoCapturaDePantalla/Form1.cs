using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using ProyectoCapturaDePantalla.face;
using ProyectoCapturaDePantalla.dao;
using ProyectoCapturaDePantalla.Images;
using System.Threading;
using ProyectoCapturaDePantalla.parser;
using ProyectoCapturaDePantalla.Domain;
using ProyectoCapturaDePantalla.utils;

namespace ProyectoCapturaDePantalla
{
    public partial class Form1 : Form
    {
        SqlConnection Conexion = DbConnection.GetConnection();

        public static string Directorio = "C:\\emotions\\imagenes";
        
        SqlCommand cmd;

        int ValorExcitacion = 0;
        int ValorValencia = 0;

        int contador = 0;
        int LapsoDeTiempo = 0;

        bool arranco;

        string NombrePrueba;
        int Seccion;
        int Identificador = 0;

        long TiempoCaptura;
        String ImagenDirectorioEscritorio;
        String ImagenDirectorioWebcam;


        AForge.Video.DirectShow.VideoCaptureDevice VideoSource;
        AForge.Video.DirectShow.FilterInfoCollection VideoSources;

        Screen[] screens2;

        public Form1()
        {
            Console.WriteLine("Inicializando");
            InitializeComponent();
            timerLapso.Stop();
            timerCaptura.Stop();
            buttonEmpezar.Focus();

            VideoSources = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            if (VideoSources != null)
            {
                foreach(Screen screens1 in Screen.AllScreens)
                {
                    comboBoxPantallas.Items.Add(screens1.DeviceName);
                }

                screens2 = Screen.AllScreens;

                comboBoxPantallas.SelectedIndex = 0;

                foreach (FilterInfo VideoSource in VideoSources)
                {
                    comboBoxWebCam.Items.Add(VideoSource.Name);
                }

                VideoSource = new AForge.Video.DirectShow.VideoCaptureDevice(VideoSources[0].MonikerString);
                VideoSource.NewFrame +=  new NewFrameEventHandler(FinalFrame_NewFrame); 
                VideoSource.Start();

                if (comboBoxWebCam.Items.Count > 0)
                {
                    comboBoxWebCam.SelectedIndex = 0;
                }
            }
            

            arranco = false;

            buttonTerminar.Enabled = false;

            try {
                Conexion.Open();

                SqlCommand cmd = new SqlCommand("select max(SECCION) AS SECCION from [NEUROSKY_IMAGENES]", Conexion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Int32.TryParse(Convert.ToString(dr["SECCION"]), out this.Seccion);
                    Seccion++;
                }
                Conexion.Close();
            } catch(Exception e) {
                Console.WriteLine("Fallo la conexion a la base");
                Console.WriteLine(e.Message);
            }           
        }

        private void buttonEmpezar_Click(object sender, EventArgs e)
        {
     
            if (textBoxName.Text == "" || textBoxName.Text == "0")
            {
                MessageBox.Show("Inserte un nombre antes de empezar la prueba");
            }
            else
            {
                Pulsador ExcitacionValencia = new Pulsador();
                ExcitacionValencia.Text = "Excitación/Valencia";
                ExcitacionValencia.ShowDialog();

                this.ValorExcitacion = ExcitacionValencia.DarValorExcitacion();
                this.ValorValencia = ExcitacionValencia.DarValorValencia();

                if (this.ValorExcitacion == 99 || this.ValorValencia == 99)
                    MessageBox.Show("Algo salio mal, cierre la ejecucion y vuelva a intentarlo");
                else
                {
                    NombrePrueba = textBoxName.Text;

                    ExcitementAndArousalDao excitementAndArousalDao = new ExcitementAndArousalDao();
                    excitementAndArousalDao.InsertExcitementAndArousal(NombrePrueba, "Al iniciar", ValorExcitacion, ValorValencia, this.Seccion);

                    //this.Insert_Excitacion_Valencia(NombrePrueba, "Al iniciar", ValorExcitacion, ValorValencia);
                    MessageBox.Show("Excitación: " + this.ValorExcitacion + " / Valencia: " + this.ValorValencia);

                    LapsoDeTiempo = 1000;
                    this.timerLapso.Interval = LapsoDeTiempo;
                    buttonTerminar.Enabled = true;
                    comboBoxPantallas.Enabled = false;
                    comboBoxWebCam.Enabled = false;
                    buttonEmpezar.Enabled = false;
                    timerLapso.Start();
                }
            }
        }

        private void timerLapso_Tick(object sender, EventArgs e)
        {
            this.buttonCapturar_Click(sender, e);
            timerLapso.Stop();
            timerLapso.Start();
        }

        private void buttonCapturar_Click(object sender, EventArgs e)
        {
            contador = 0;
            this.timerCaptura.Start();
        }

        private void timerCaptura_Tick(object sender, EventArgs e)
        {
            if (contador == 1)
            {
                arranco = true;

                this.Identificador++;
                
                Bitmap Imgb = new Bitmap(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Width, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Height, PixelFormat.Format32bppArgb);
                Graphics graf = Graphics.FromImage(Imgb);
                
                graf.CopyFromScreen(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.X, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Y, 0, 0, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Size, CopyPixelOperation.SourceCopy);
                
                pictureBoxImg.Image = Imgb;

                if (!Directory.Exists(Directorio))
                {
                    Directory.CreateDirectory(Directorio);
                }

                TiempoCaptura = (long)DateTime.UtcNow.Ticks;

                ImagenDirectorioEscritorio = String.Format(@"{0}\Escritorio-{1}.jpg", Directorio, TiempoCaptura);
                Imgb.Save(ImagenDirectorioEscritorio, ImageFormat.Bmp);

                ImagenDirectorioWebcam = String.Format(@"{0}\WebCam-{1}.jpg", Directorio, TiempoCaptura);
                pictureBoxWebCam.Image.Save(ImagenDirectorioWebcam, System.Drawing.Imaging.ImageFormat.Png);

                ImagesDao imagesDao = new ImagesDao();
                imagesDao.InsertImages(this.NombrePrueba, this.Seccion, this.Identificador, ImagenDirectorioEscritorio, ImagenDirectorioWebcam);

                timerCaptura.Stop();
            }
            contador++;
        }

        private async void CerrarSeccion()
        {
            if (arranco == true)
            {
                DialogResult dialogResult = MessageBox.Show("Desea iniciar el proceso de reconocimiento facial ahora?", "Confirmación", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    await InitFaceRecognition(Seccion);
                }
                Seccion++;
                Identificador = 0;
            }
            arranco = false;
        }

        private void buttonTerminar_Click(object sender, EventArgs e)
        {
            timerLapso.Stop();
            //this.CallSP();
            this.FinalizarCapturas();
        }

        public int CallSP(string SP)
        {
            string NombreQuery;
            int resultado;
            if (NombrePrueba == null)
                NombreQuery = textBoxName.Text;
            else
                NombreQuery = NombrePrueba;

            Conexion.Open();
            SqlCommand cmd = new SqlCommand(SP, Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = NombreQuery;
            resultado = cmd.ExecuteNonQuery();
            Conexion.Close();

            return resultado;
        }

        public void FinalizarCapturas()
        {
            if (textBoxName.Text == "" || textBoxName.Text == "0")
            {
                MessageBox.Show("Inserte un nombre antes de empezar la prueba");
            }
            else
            {
                Pulsador ExcitacionValencia = new Pulsador();
                ExcitacionValencia.Text = "Excitación/Valencia";
                ExcitacionValencia.ShowDialog();

                this.ValorExcitacion = ExcitacionValencia.DarValorExcitacion();
                this.ValorValencia = ExcitacionValencia.DarValorValencia();

                if (this.ValorExcitacion == 99 || this.ValorValencia == 99)
                    MessageBox.Show("Algo salio mal, cierre la ejecucion y vuelva a intentarlo");
                else
                {
                    NombrePrueba = textBoxName.Text;

                    ExcitementAndArousalDao excitementAndArousalDao = new ExcitementAndArousalDao();
                    excitementAndArousalDao.InsertExcitementAndArousal(NombrePrueba, "Al Finalizar", ValorExcitacion, ValorValencia, this.Seccion);

                    //this.Insert_Excitacion_Valencia(NombrePrueba, "Al Finalizar", ValorExcitacion, ValorValencia);
                    MessageBox.Show("Excitación: " + this.ValorExcitacion + " / Valencia: " + this.ValorValencia);

                    this.CerrarSeccion();

                    buttonTerminar.Enabled = false;
                    buttonEmpezar.Enabled = true;
                    comboBoxPantallas.Enabled = true;
                    comboBoxWebCam.Enabled = true;
                    buttonEmpezar.Focus();
                }
            }
        }

        private void comboBoxWebCam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VideoSources != null)
            {
                if (VideoSource.IsRunning)
                    VideoSource.Stop();
                VideoSource = new AForge.Video.DirectShow.VideoCaptureDevice(VideoSources[comboBoxWebCam.SelectedIndex].MonikerString);
                VideoSource.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                VideoSource.Start();
            }
        }

        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBoxWebCam.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VideoSource.IsRunning == true)
                VideoSource.Stop();
        }

        private void buttonRunSP_Click(object sender, EventArgs e)
        {
            //this.CallSP();
            MessageBox.Show("El numero de registros afectados en la tabla general fueron: " + this.CallSP("SP_CargarDatos"));
            MessageBox.Show("El numero de registros afectados en la tabla Excitacion-Valencia fueron: " + this.CallSP("SP_CargarDatos_Excitacion_Valencia"));
        }

        private async Task emotionButton_ClickAsync(object sender, EventArgs e)
        {
            string promptValue = new Prompt("Reconocimiento facial", "Ingrese el número de sección de la prueba de la que desea hacer el reconocimiento").show();
            if (int.TryParse(promptValue, out int section))
                await InitFaceRecognition(section);

            //if (int.TryParse(promptValue, out section))
            //    {
            //    using (LoadingView loadingView = new LoadingView(async () => { await InitFaceRecognition(section); }))
            //    {
            //        loadingView.ShowDialog(this);
            //    }
            //}
        }

        private async Task InitFaceRecognition(int section)
        {
            FaceService faceService = new FaceService();
            try
            {
                await faceService.DetectFacesEmotionByBulk(this.GetImages(section, 1));
                MessageBox.Show("El proceso de detección de imagenes finalizó!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error procesando las emociones!");
                Console.WriteLine(ex.Message);
                MessageBox.Show("Ocurrió un error procesando las emociones");
            }
        }

        //If bulkLimit is 0, get all images
        private List<FaceImage> GetImages(int section, int bulkLimit)
        {
            List<FaceImage> images = new List<FaceImage>();

            try
            {
                ImagesDao imagesDao = new ImagesDao();
                images = imagesDao.GetImages(section);

                if (images.Count > 0 && bulkLimit > 0)
                {
                    return images.GetRange(0, bulkLimit);
                }

                return images;
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw e;
            }
        }

        private void biometricsBtn_Click(object sender, EventArgs e)
        {
            string promptValue = new Prompt("Procesamiento biometrico", "Ingrese el número de sección de la prueba de la que desea hacer el procesamiento de datos biometricos").show();
            if (!int.TryParse(promptValue, out int section))
            {
                throw new Exception("Error al intentar parsear el número de seccción. Verifique que los datos sean correctos");
            }

            ParserService parserService = new ParserService();
            try
            {
                SkinMeasurement skinMeasurement = parserService.ParseCsvSkinMeasurement(SkinMeasurement.PATH, SkinMeasurement.FILE_NAME, SkinMeasurement.CSV_KEY);
                SkinDao skinDao = new SkinDao();
                skinDao.SaveSkinMeasurement(skinMeasurement, section);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                PulseMeasurement pulseMeasurement = parserService.ParseCsvPulseMeasurement(PulseMeasurement.PATH, PulseMeasurement.FILE_NAME, PulseMeasurement.CSV_KEY);
                PulseDao pulseDao = new PulseDao();
                pulseDao.SavePulseMeasurement(pulseMeasurement, section);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

}
