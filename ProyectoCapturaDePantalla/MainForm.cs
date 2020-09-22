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
using ProyectoCapturaDePantalla.Domain.Session;
using System.Configuration;
using ProyectoCapturaDePantalla.Domain.SAM;
using ProyectoCapturaDePantalla.Domain.Phase;
using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using ProyectoCapturaDePantalla.Domain.TestSet;

namespace ProyectoCapturaDePantalla
{
    public partial class MainForm : Form
    {
        /* Instance Variables */
        SqlConnection Conexion = DbConnection.GetConnection();
        Session currentSession;
        int contador = 0;
        int Seccion;
        int Identificador = 0;
        AForge.Video.DirectShow.VideoCaptureDevice VideoSource;
        AForge.Video.DirectShow.FilterInfoCollection VideoSources;
        Screen[] screens2;
        int defaultTestSet = 1;

        public MainForm()
        {
            InitializeComponent();
            timerLapso.Stop();
            timerCaptura.Stop();
            buttonEmpezar.Focus();

            //TestSet testSet = TestSetDao.GetTestSet(defaultTestSet);

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

            buttonTerminar.Enabled = false;

            try {
                Conexion.Open();

                SqlCommand cmd = new SqlCommand("select max(SECCION) AS SECCION from [NEUROSKY_IMAGENES]", Conexion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int.TryParse(Convert.ToString(dr["SECCION"]), out this.Seccion);
                    Seccion++;
                }
                Conexion.Close();
            } catch(Exception e) {
                Console.WriteLine("Fallo la conexion a la base");
                Console.WriteLine(e.Message);
            }
        }

        private async void buttonEmpezar_Click(object sender, EventArgs e)
        {
            string testName = textBoxName.Text;

            if (testName == "")
            {
                MessageBox.Show("Inserte un nombre antes de empezar la prueba");
            }
            else
            {
                SessionDao sessionDao = new SessionDao();
                currentSession = new Session(testName);

                try
                {
                    sessionDao.SaveSession(currentSession);
                }
                catch (SqlException ex)
                {
                    if (ex.ErrorCode == 2146232060 || ex.ErrorCode == -2146232060)
                    {
                        MessageBox.Show("Ya existe una prueba con ese nombre. Por favor elija otro");
                        return;
                    }
                    throw ex;
                }

                SessionEventDao sessionEventDao = new SessionEventDao();

                SessionEvent sessionStart = new SessionEvent(currentSession.Id, currentSession.TestName, EventTypes.SESSION_START, DateTime.Now);
                sessionEventDao.SaveSessionEvent(sessionStart);

                requestSAM();

                SessionEvent initialSAM = new SessionEvent(currentSession.Id, currentSession.TestName, EventTypes.INITIAL_SAM, DateTime.Now);
                sessionEventDao.SaveSessionEvent(initialSAM);

                timerLapso.Interval = 1000;
                buttonTerminar.Enabled = true;
                comboBoxPantallas.Enabled = false;
                comboBoxWebCam.Enabled = false;
                buttonEmpezar.Enabled = false;
                timerLapso.Start();

                TestSet testSet = TestSetDao.GetTestSet(defaultTestSet);

                foreach (PhaseBase phase in testSet.Phases)
                {
                    sessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("INIT_", phase.ValenceArrousalQuadrant, "_", phase.Id.ToString()), DateTime.Now));

                    //if(phase.StimuliType == ImagePhase.IAP_TYPE)
                    //{
                    //    var imagePhase = (ImagePhase)phase;
                    //    await startPresentation(imagePhase.Iaps, ConfigurationManager.AppSettings["iaps-path"]);
                    //}

                    if(phase.StimuliType == VideoPhase.DEVO_TYPE)
                    {
                        var videoPhase = (VideoPhase)phase;
                        await startPresentation(videoPhase.Videos, ConfigurationManager.AppSettings["devo-path"]);
                    }
                    

                    sessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("END_", phase.ValenceArrousalQuadrant, "_", phase.Id.ToString()), DateTime.Now));

                    requestSAM();
                    sessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("SAM_", phase.ValenceArrousalQuadrant), DateTime.Now));
                }
            }
        }

        private SAM requestSAM()
        {
            SAMForm SAMForm = new SAMForm();
            SAMForm.ShowDialog();
            SAM response = SAMForm.getSAMResponse();
            ValenceAndArousalDao excitementAndArousalDao = new ValenceAndArousalDao();
            excitementAndArousalDao.InsertExcitementAndArousal(currentSession.TestName, EventTypes.INITIAL_SAM, response.Valence, response.Arousal, this.Seccion);
            return response;
        }

        private void timerLapso_Tick(object sender, EventArgs e)
        {
            contador = 0;
            timerCaptura.Start();
            timerLapso.Stop();
            timerLapso.Start();
        }

        private void timerCaptura_Tick(object sender, EventArgs e)
        {
            if (contador == 1)
            {
                this.Identificador++;
                
                Bitmap Imgb = new Bitmap(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Width, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Height, PixelFormat.Format32bppArgb);
                Graphics graf = Graphics.FromImage(Imgb);
                
                graf.CopyFromScreen(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.X, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Y, 0, 0, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Size, CopyPixelOperation.SourceCopy);
                
                pictureBoxImg.Image = Imgb;

                string imagesPath = ConfigurationManager.AppSettings["images-path"];
                string desktopPath = string.Concat(imagesPath, "\\desktop");
                string webcamPath = string.Concat(imagesPath, "\\webcam");
                Directory.CreateDirectory(desktopPath);
                Directory.CreateDirectory(webcamPath);

                long tiempoCaptura = DateTime.UtcNow.Ticks;

                string desktopScreenshot = string.Format(@"{0}\Escritorio-{1}.jpg", desktopPath, tiempoCaptura);
                Imgb.Save(desktopScreenshot, ImageFormat.Bmp);

                string webcamPicture = string.Format(@"{0}\WebCam-{1}.jpg", webcamPath, tiempoCaptura);
                pictureBoxWebCam.Image.Save(webcamPicture, ImageFormat.Png);

                ImagesDao imagesDao = new ImagesDao();
                imagesDao.InsertImages(currentSession.TestName, this.Seccion, this.Identificador, desktopScreenshot, webcamPicture);

                timerCaptura.Stop();
            }
            contador++;
        }

        private async void CerrarSeccion()
        {
            DialogResult dialogResult = MessageBox.Show("Desea iniciar el proceso de reconocimiento facial ahora?", "Confirmación", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Enabled = false;
                await InitFaceRecognition(Seccion);
                Enabled = true;
            }
            Seccion++;
            Identificador = 0;
        }

        private void buttonTerminar_Click(object sender, EventArgs e)
        {
            SessionEventDao sessionEventDao = new SessionEventDao();
            sessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "END_TEST", DateTime.Now));

            timerLapso.Stop();
            //this.CallSP();
            CerrarSeccion();
            buttonTerminar.Enabled = false;
            comboBoxPantallas.Enabled = true;
            comboBoxWebCam.Enabled = true;
            buttonEmpezar.Enabled = true;
        }

        public int CallSP(string SP)
        {
            string NombreQuery = currentSession.TestName;

            Conexion.Open();
            SqlCommand cmd = new SqlCommand(SP, Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = NombreQuery;
            int resultado = cmd.ExecuteNonQuery();
            Conexion.Close();

            return resultado;
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
            {
                Enabled = false;
                await InitFaceRecognition(section);
                Enabled = true;
            }
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

        private async Task startPresentation(List<IAP> iapsList, string path)
        {
            ImageDisplay imageDisplay = new ImageDisplay(path);
            imageDisplay.WindowState = FormWindowState.Maximized;
            imageDisplay.Show();

            await Task.Run(async () =>
            {
                foreach (IAP iaps in iapsList)
                {
                    imageDisplay.ChangeImage(string.Concat(iaps.IdIaps, ".jpg"));
                    await Task.Delay(2000);
                }
            });

           imageDisplay.Close();
        }

        private async Task startPresentation(List<DEVO> videoList, string path)
        {
            VideoDisplay videoDisplay = new VideoDisplay(path);
            videoDisplay.WindowState = FormWindowState.Maximized;
            videoDisplay.Show();

            await Task.Run(async () =>
            {
                foreach (DEVO video in videoList)
                {
                    videoDisplay.ChangeVideo(string.Concat(video.Id, ".mp4"));
                    await Task.Delay(new TimeSpan((long)video.LengthInMs + 1000));
                }
            });

            videoDisplay.Close();
        }
    }
}
