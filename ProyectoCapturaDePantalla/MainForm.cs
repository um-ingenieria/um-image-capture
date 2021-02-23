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
using ProyectoCapturaDePantalla.Domain.Skin;

namespace ProyectoCapturaDePantalla
{
    public partial class MainForm : Form
    {
        /* Instance Variables */
        SqlConnection Conexion = DbConnection.GetConnection();
        Session currentSession;
        int contador = 0;
        int imageId = 0;
        AForge.Video.DirectShow.VideoCaptureDevice VideoSource;
        AForge.Video.DirectShow.FilterInfoCollection VideoSources;
        Screen[] screens2;
        int defaultTestSet = 3;

        VideoDisplay videoPlayer;
        ImageDisplay imagePlayer;

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
                currentSession = new Session(testName, defaultTestSet);

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

                SessionEvent sessionStart = new SessionEvent(currentSession.Id, currentSession.TestName, EventTypes.SESSION_START, DateTime.Now);
                SessionEventDao.SaveSessionEvent(sessionStart);

                requestSAM(EventTypes.INITIAL_SAM);

                SessionEvent initialSAM = new SessionEvent(currentSession.Id, currentSession.TestName, EventTypes.INITIAL_SAM, DateTime.Now);
                SessionEventDao.SaveSessionEvent(initialSAM);

                timerLapso.Interval = 1000;
                buttonTerminar.Enabled = true;
                comboBoxPantallas.Enabled = false;
                comboBoxWebCam.Enabled = false;
                buttonEmpezar.Enabled = false;
                timerLapso.Start();

                TestSet testSet = TestSetDao.GetTestSet(defaultTestSet);

                foreach (Phase phase in testSet.Phases)
                {
                    SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("INIT_", phase.ValenceArrousalQuadrant, "_", phase.Id), DateTime.Now));

                    await showStimulis(phase);

                    SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("END_", phase.ValenceArrousalQuadrant, "_", phase.Id), DateTime.Now));

                    requestSAM(string.Concat("SAM_", phase.ValenceArrousalQuadrant, "_", phase.Id));
                    SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, string.Concat("SAM_", phase.ValenceArrousalQuadrant, "_", phase.Id), DateTime.Now));
                }
            }
        }


        private async Task showStimulis(Phase phase)
        {
            imagePlayer = new ImageDisplay(ConfigurationManager.AppSettings["iaps-path"]);
            imagePlayer.WindowState = FormWindowState.Maximized;
            List<DimensionalStimuli> listOfThings = phase.Stimulis.OrderBy(i => Guid.NewGuid()).ToList();

            foreach (DimensionalStimuli stimuli in listOfThings)
            {
                if (stimuli.Type == IAP.IAP_TYPE)
                {
                    imagePlayer.Show();

                    await Task.Run(async () =>
                    {
                        SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "INIT_STIMULI", DateTime.Now, stimuli));
                        imagePlayer.ChangeImage(string.Concat(stimuli.Id, ".jpg"));
                        await Task.Delay(2000);
                        SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "END_STIMULI", DateTime.Now, stimuli));
                    });
                }
                else if (stimuli.Type == DEVO.DEVO_TYPE)
                {
                    imagePlayer.ClearImage();
                    SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "INIT_STIMULI", DateTime.Now, stimuli));
                    videoPlayer = new VideoDisplay(ConfigurationManager.AppSettings["devo-path"]);
                    videoPlayer.WindowState = FormWindowState.Maximized;
                    videoPlayer.play(string.Concat(stimuli.Id, ".mp4"));
                    videoPlayer.ShowDialog();
                    SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "END_STIMULI", DateTime.Now, stimuli));
                }
            }

            imagePlayer.Close();
        }

        private SAM requestSAM(string samType)
        {
            SAMForm SAMForm = new SAMForm();
            SAMForm.ShowDialog();
            SAM response = SAMForm.getSAMResponse();
            ValenceAndArousalDao excitementAndArousalDao = new ValenceAndArousalDao();
            excitementAndArousalDao.InsertExcitementAndArousal(currentSession.TestName, samType, response.Valence, response.Arousal, currentSession.Id);
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
                this.imageId++;
                
                Bitmap Imgb = new Bitmap(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Width, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Height, PixelFormat.Format32bppArgb);
                Graphics graf = Graphics.FromImage(Imgb);
                
                graf.CopyFromScreen(screens2[comboBoxPantallas.SelectedIndex].WorkingArea.X, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Y, 0, 0, screens2[comboBoxPantallas.SelectedIndex].WorkingArea.Size, CopyPixelOperation.SourceCopy);
                
                pictureBoxImg.Image = Imgb;

                string imagesPath = ConfigurationManager.AppSettings["images-path"];
                string desktopPath = string.Concat(imagesPath, "\\desktop\\", currentSession.TestName);
                string webcamPath = string.Concat(imagesPath, "\\webcam\\", currentSession.TestName);
                Directory.CreateDirectory(desktopPath);
                Directory.CreateDirectory(webcamPath);

                long tiempoCaptura = DateTime.UtcNow.Ticks;

                string desktopScreenshot = string.Format(@"{0}\Escritorio-{1}.jpg", desktopPath, tiempoCaptura);
                Imgb.Save(desktopScreenshot, ImageFormat.Bmp);

                string webcamPicture = string.Format(@"{0}\WebCam-{1}.jpg", webcamPath, tiempoCaptura);
                pictureBoxWebCam.Image.Save(webcamPicture, ImageFormat.Png);

                ImagesDao imagesDao = new ImagesDao();
                imagesDao.InsertImages(currentSession.TestName, currentSession.Id, this.imageId, desktopScreenshot, webcamPicture);

                timerCaptura.Stop();
            }
            contador++;
        }

        private async void CloseSession()
        {
            DialogResult dialogResult = MessageBox.Show("Desea iniciar el proceso de reconocimiento facial ahora?", "Confirmación", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string imagesNamePrompt = new Prompt("Reconocimiento emocional facial", "Ingrese el listado de nombres de imaegenes separados por una coma(,) (sin dejar espacios)").show();
                if (imagesNamePrompt != "")
                {
                    try
                    {
                        Enabled = false;
                        await InitFaceRecognition(currentSession.Id, castImgesNames(imagesNamePrompt));
                        Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Error este campo no puede estar vacio");
                    }
                }
                else
                {
                    MessageBox.Show("Error este campo no puede estar vacio");
                }
            }
            imageId = 0;
        }

        private void buttonTerminar_Click(object sender, EventArgs e)
        {
            SessionEventDao.SaveSessionEvent(new SessionEvent(currentSession.Id, currentSession.TestName, "END_TEST", DateTime.Now));

            timerLapso.Stop();
            //this.CallSP();
            CloseSession();
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
            try
            {
                pictureBoxWebCam.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            
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
            int selectedSessionId;
            string promptValue = new Prompt("Reconocimiento facial", "Ingrese el número de sección de la prueba de la que desea hacer el reconocimiento").show();
            if (int.TryParse(promptValue, out selectedSessionId))
            {
                string imagesNamePrompt = new Prompt("Reconocimiento emocional facial", "Ingrese el listado de nombres de imaegenes separados por una coma(,) (sin dejar espacios)").show();
                if (imagesNamePrompt != "")
                {
                    try
                    {
                        Enabled = false;
                        await InitFaceRecognition(selectedSessionId, castImgesNames(imagesNamePrompt));
                        Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Error este campo no puede estar vacio");
                    }
                } else
                {
                    MessageBox.Show("Error este campo no puede estar vacio");
                }
            } else
            {
                MessageBox.Show("Error al intentar parsear el número de seccción. Verifique que los datos sean correctos");
            }
        }

        private static List<string> castImgesNames(string imagesNamePrompt)
        {
            string imagesNameTrimmed = String.Concat(imagesNamePrompt.Where(c => !Char.IsWhiteSpace(c)));
            string[] imagesName = imagesNameTrimmed.Split(',');
            return imagesName.ToList();
        }

        private async Task InitFaceRecognition(int sessionId, List<string> imagesName)
        {
            FaceService faceService = new FaceService();
            try
            {
                await faceService.DetectFacesEmotionByBulk(this.GetImages(sessionId, imagesName, 50));
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
        private List<FaceImage> GetImages(int sessionId, List<string> imagesName, int bulkLimit)
        {
            List<FaceImage> images = new List<FaceImage>();

            try
            {
                ImagesDao imagesDao = new ImagesDao();
                images = imagesDao.GetImages(sessionId, imagesName);

                if (images.Count > 0 && bulkLimit > 0 && images.Count > bulkLimit)
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
            string promptValue = new Prompt("Procesamiento biometrico", "Ingrese el número de session de la prueba de la que desea hacer el procesamiento de datos biometricos").show();
            if (!int.TryParse(promptValue, out int sessionId))
            {
                throw new Exception("Error al intentar parsear el número de seccción. Verifique que los datos sean correctos");
            }

            ParserService parserService = new ParserService();
            SkinMeasurement skinMeasurement = null;
            try
            {
                skinMeasurement = parserService.ParseCsvSkinMeasurement(SkinMeasurement.PATH, SkinMeasurement.FILE_NAME, SkinMeasurement.CSV_KEY);
                SkinDao skinDao = new SkinDao();
                //skinDao.SaveSkinMeasurement(skinMeasurement, sessionId);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            PulseMeasurement pulseMeasurement = null;
            try
            {
                pulseMeasurement = parserService.ParseCsvPulseMeasurement(PulseMeasurement.PATH, PulseMeasurement.FILE_NAME, PulseMeasurement.CSV_KEY);
                PulseDao pulseDao = new PulseDao();
                //pulseDao.SavePulseMeasurement(pulseMeasurement, sessionId);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            createHrSkinAndArousalMesurement(sessionId, pulseMeasurement, skinMeasurement);
        }

        private void createHrSkinAndArousalMesurement(int sessionId, PulseMeasurement pulseMeasurement, SkinMeasurement skinMeasurement)
        {
            // Juntamos los eventos de inicio y fin de estimulo, asociados a su nivel de valence y arousal
            List<SessionEvent> sessionEvents = SessionEventDao.GetStimuliEventsBySessionId(sessionId);

            // Filtrmamos los eventos de SAM
            List<SessionEvent> samEvents = sessionEvents.FindAll(item => item.TestEvent.Contains("SAM"));

            // Filtramos eventos de estimulos
            List<SessionEvent> stimuliEvents = sessionEvents.FindAll(item => item.TestEvent.Contains("STIMULI"));

            // Unificamos los registris de HR y Skin en una misma linea de tiempo
            List<BiometricModelData> biometricsModelData = mergeListAWithListB(pulseMeasurement.PulseStatistics, skinMeasurement.SkinStatistics);

            // A cada medicion de HR y SKIN entre un par de eventos INIT_STIMULI y END_STIMULI, le asignamos el valor de valence-arousal del estimulo asociado a esos eventos.
            for (int i = 0; i < stimuliEvents.Count; i += 2)
            {
                if (!(stimuliEvents[i].TestEvent == "INIT_STIMULI" && stimuliEvents[i + 1].TestEvent == "END_STIMULI"))
                {
                    throw new Exception("Los tipos de evento no son los correctos");
                }

                SetArousalAndValenceInInterval(biometricsModelData, stimuliEvents[i].EventDate, stimuliEvents[i + 1].EventDate, stimuliEvents[i].Stimuli);
            }

            // Etiquetar fases. Empieza desde el índice = 1 para saltear el evento "INITIAL_SAM"
            for (int i = 1; i < samEvents.Count; i++)
            {
                string phaseName = samEvents[i].TestEvent.Replace("SAM_", "");
                DateTime? sinceDate = null;
                if (i > 0)
                {
                    sinceDate = samEvents[i - 1].EventDate;
                }
                
                SetPhaseNameInInterval(biometricsModelData, sinceDate, samEvents[i].EventDate, phaseName);
            }

            // Descartar mediciones de fases que no coincidan con el SAM
            ValenceAndArousalDao valenceArousalDao = new ValenceAndArousalDao();
            List<SAM> samList = valenceArousalDao.GetSAMsBySessionId(sessionId);

            markSamOutliers(biometricsModelData, samList);
            // Save model to csv
            BiometricModelDataDao.SaveBiometricModelDataToCsv(biometricsModelData);

            // Informe final
            MessageBox.Show("Mostrar informe final");
            // SAM descartadas, cantidad de registros procesados / descartados, errores, success...
        }

        private static void markSamOutliers(List<BiometricModelData> biometricsModelData, List<SAM> samList)
        {
            foreach (SAM sam in samList)
            {
                if (!sam.samMatchesPhaseValenceAndArousal())
                {
                    string type = sam.Type.Replace("SAM_", "");
                    foreach (BiometricModelData biometric in biometricsModelData)
                    {
                        //Si phase es igual a NULL es porque son datos posteriores a que termine la prueba, los amrcamos como false apra despues descartarlos
                        if (biometric.PhaseName == null || biometric.PhaseName.Equals(type))
                        {
                            biometric.MatchesSam = false;
                        }
                    }
                }
            }
        }


        private void SetPhaseNameInInterval(List<BiometricModelData> biometrics, DateTime? dateSince, DateTime dateTo, string phaseName)
        {
            int i = 0;
            while ( (i < biometrics.Count - 1) && biometrics[i].TimeStamp.CompareTo(dateTo) < 0)
            {
                if (dateSince == null || biometrics[i].TimeStamp.CompareTo(dateSince) > 0)
                {
                    biometrics[i].PhaseName = phaseName;
                }
                i++;
            }
        }

        private void SetArousalAndValenceInInterval(List<BiometricModelData> biometrics, DateTime dateSince, DateTime dateTo, DimensionalStimuli stimuli)
        {
            int i = 0;
            while(i < biometrics.Count -1 && biometrics[i].TimeStamp.CompareTo(dateTo) < 0)
            {
                if (biometrics[i].TimeStamp.CompareTo(dateSince) > 0)
                {
                    biometrics[i].ArousalMean = stimuli.ArousalMean;
                    biometrics[i].ValenceMean = stimuli.ValenceMean;
                    biometrics[i].ValenceSD = stimuli.ValenceSD;
                    biometrics[i].ArousalSD = stimuli.ArousalSD;
                }
                i++;
            }
        }

        private List<BiometricModelData> mergeListAWithListB(List<PulseStatistic> pulseStatistics, List<SkinStatistic> skinStatistics)
        {
            int i = 0;
            List<BiometricModelData> biometricsModelData = new List<BiometricModelData>();
            foreach (PulseStatistic pulseStatistic in pulseStatistics)
            {
                while ((i < skinStatistics.Count - 2) && (skinStatistics[i].AbsoluteTime.CompareTo(pulseStatistic.AbsoluteTime) <= 0))
                {
                    i++;
                }

                biometricsModelData.Add(new BiometricModelData(pulseStatistic.AbsoluteTime, pulseStatistic.HR, pulseStatistic.RR, pulseStatistic.HRV, skinStatistics[i].MicroSiemens, skinStatistics[i].SCR, skinStatistics[i].SCR_MIN));
            }

            return biometricsModelData;
        }
    }
}
