using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using ProyectoCapturaDePantalla.dao;
using ProyectoCapturaDePantalla.Images;

namespace ProyectoCapturaDePantalla.face
{
    public class FaceService
    {
        public static int BULK_LIMIT = 10;
        public static string SUBSCRIPTION_KEY = "968cb12edb324b7b87678753e6537f03";
        public static string SUBSCRIPTION_ENDPOINT = "https://emotion-recognition-api.cognitiveservices.azure.com/";
        public static int NO_BULK_LMIT = 0;

        IFaceClient client;

        public FaceService()
        {
            Console.WriteLine("Servicio creado, no soy un singleton");

            // Authenticate.
            client = Authenticate(SUBSCRIPTION_ENDPOINT, SUBSCRIPTION_KEY);
        }

        public async Task DetectFacesEmotionByBulk(List<FaceImage> images)
        {
            await DetectFaceEmotionsByBulk(client, images, RecognitionModel.Recognition01, BULK_LIMIT);
        }

        public async Task DetectFacesEmotion(List<FaceImage> images)
        {
            await DetectFaceEmotions(client, images, RecognitionModel.Recognition01);
        }

        public static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        public static async Task DetectAllFaceAtributes (IFaceClient client, string url, string recognitionModel)
        {
            Console.WriteLine("========DETECT FACES========");
            Console.WriteLine();

            // Create a list of images
            List<string> imageFileNames = Directory.GetFiles("C:\\imagenes", "*.*", SearchOption.AllDirectories).ToList();

            imageFileNames = imageFileNames.Where(item => item.Contains("WebCam")).ToList();

            if (imageFileNames.Count >= 10)
            {
                // Intente tirar directamentela excepción, pero no te muestra el message en la consola....
                string message = "Límite de imágenes excedido, no se puede mandar más de 10 imágenes";
                Console.WriteLine("ERROR: " + message);
                throw new Exception(message);
            }

            foreach (var imageFileName in imageFileNames)
            {
                IList<DetectedFace> detectedFaces;

                // Detect faces with all attributes from image url.
                FileStream file = new FileStream(imageFileName, FileMode.Open);
                detectedFaces = await client.Face.DetectWithStreamAsync(
                file,
                returnFaceLandmarks: true,
                returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile,},
                recognitionModel: recognitionModel);

                Console.WriteLine($"{detectedFaces.Count} face(s) detected from image `{imageFileName}`.");

                // Parse and print all attributes of each detected face.
                foreach (var face in detectedFaces)
                {
                    Console.WriteLine($"Face attributes for {imageFileName}:");

                    // Get bounding box of the faces
                    Console.WriteLine($"Rectangle(Left/Top/Width/Height) : {face.FaceRectangle.Left} {face.FaceRectangle.Top} {face.FaceRectangle.Width} {face.FaceRectangle.Height}");

                    // Get accessories of the faces
                    List<Accessory> accessoriesList = (List<Accessory>)face.FaceAttributes.Accessories;
                    int count = face.FaceAttributes.Accessories.Count;
                    string accessory; string[] accessoryArray = new string[count];
                    if (count == 0) { accessory = "NoAccessories"; }
                    else
                    {
                        for (int i = 0; i < count; ++i) { accessoryArray[i] = accessoriesList[i].Type.ToString(); }
                        accessory = string.Join(",", accessoryArray);
                    }
                    Console.WriteLine($"Accessories : {accessory}");

                    // Get face other attributes
                    Console.WriteLine($"Age : {face.FaceAttributes.Age}");
                    Console.WriteLine($"Blur : {face.FaceAttributes.Blur.BlurLevel}");

                    // Get emotion on the face
                    string emotionType = string.Empty;
                    double emotionValue = 0.0;
                    Emotion emotion = face.FaceAttributes.Emotion;
                    if (emotion.Anger > emotionValue) { emotionValue = emotion.Anger; emotionType = "Anger"; }
                    if (emotion.Contempt > emotionValue) { emotionValue = emotion.Contempt; emotionType = "Contempt"; }
                    if (emotion.Disgust > emotionValue) { emotionValue = emotion.Disgust; emotionType = "Disgust"; }
                    if (emotion.Fear > emotionValue) { emotionValue = emotion.Fear; emotionType = "Fear"; }
                    if (emotion.Happiness > emotionValue) { emotionValue = emotion.Happiness; emotionType = "Happiness"; }
                    if (emotion.Neutral > emotionValue) { emotionValue = emotion.Neutral; emotionType = "Neutral"; }
                    if (emotion.Sadness > emotionValue) { emotionValue = emotion.Sadness; emotionType = "Sadness"; }
                    if (emotion.Surprise > emotionValue) { emotionType = "Surprise"; }
                    Console.WriteLine($"Emotion : {emotionType}");

                    // Get more face attributes
                    Console.WriteLine($"Exposure : {face.FaceAttributes.Exposure.ExposureLevel}");
                    Console.WriteLine($"FacialHair : {string.Format("{0}", face.FaceAttributes.FacialHair.Moustache + face.FaceAttributes.FacialHair.Beard + face.FaceAttributes.FacialHair.Sideburns > 0 ? "Yes" : "No")}");
                    Console.WriteLine($"Gender : {face.FaceAttributes.Gender}");
                    Console.WriteLine($"Glasses : {face.FaceAttributes.Glasses}");

                    // Get hair color
                    Hair hair = face.FaceAttributes.Hair;
                    string color = null;
                    if (hair.HairColor.Count == 0) { if (hair.Invisible) { color = "Invisible"; } else { color = "Bald"; } }
                    HairColorType returnColor = HairColorType.Unknown;
                    double maxConfidence = 0.0f;
                    foreach (HairColor hairColor in hair.HairColor)
                    {
                        if (hairColor.Confidence <= maxConfidence) { continue; }
                        maxConfidence = hairColor.Confidence; returnColor = hairColor.Color; color = returnColor.ToString();
                    }
                    Console.WriteLine($"Hair : {color}");

                    // Get more attributes
                    Console.WriteLine($"HeadPose : {string.Format("Pitch: {0}, Roll: {1}, Yaw: {2}", Math.Round(face.FaceAttributes.HeadPose.Pitch, 2), Math.Round(face.FaceAttributes.HeadPose.Roll, 2), Math.Round(face.FaceAttributes.HeadPose.Yaw, 2))}");
                    Console.WriteLine($"Makeup : {string.Format("{0}", (face.FaceAttributes.Makeup.EyeMakeup || face.FaceAttributes.Makeup.LipMakeup) ? "Yes" : "No")}");
                    Console.WriteLine($"Noise : {face.FaceAttributes.Noise.NoiseLevel}");
                    Console.WriteLine($"Occlusion : {string.Format("EyeOccluded: {0}", face.FaceAttributes.Occlusion.EyeOccluded ? "Yes" : "No")} " +
                        $" {string.Format("ForeheadOccluded: {0}", face.FaceAttributes.Occlusion.ForeheadOccluded ? "Yes" : "No")}   {string.Format("MouthOccluded: {0}", face.FaceAttributes.Occlusion.MouthOccluded ? "Yes" : "No")}");
                    Console.WriteLine($"Smile : {face.FaceAttributes.Smile}");
                    Console.WriteLine();
                }
            }
        }

        private async Task DetectFaceEmotionsByBulk (IFaceClient client, List<FaceImage> images, string recognitionModel, int bulkLimit)
        {
            //TODO: VALIDATE BULK
            await this.GetFaceEmotionsAndSave(client, recognitionModel, images);
        }

        private async Task DetectFaceEmotions(IFaceClient client, List<FaceImage> images, string recognitionModel)
        {
            await this.GetFaceEmotionsAndSave(client, recognitionModel, images);
        }

        private async Task GetFaceEmotionsAndSave(IFaceClient client, string recognitionModel, List<FaceImage> images)
        {
            foreach (FaceImage image in images)
            {
                IList<DetectedFace> detectedFaces;

                // Detect faces with all attributes from image url.
                FileStream file = new FileStream(image.Path, FileMode.Open);
                detectedFaces = await client.Face.DetectWithStreamAsync(file, returnFaceLandmarks: true,
                                        returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Emotion, },
                                        recognitionModel: recognitionModel);

                Console.WriteLine($"{detectedFaces.Count} face(s) detected from image `{image.Path}`.");

                foreach (var face in detectedFaces)
                {
                    // Get emotion on the face
                    Emotion emotion = face.FaceAttributes.Emotion;
                    string emotionType = GetEmotionType(emotion);
                    //TODO: pasar id y section
                    EmotionsDao.SaveEmotion(emotion, image.Section, image.Id, CalculateValence(emotion));
                }
            }
        }

        private double CalculateValence(Emotion emotion)
        {
            double positiveValues = 0;
            double negativeValues = 0;

            positiveValues += emotion.Happiness;
            negativeValues += emotion.Sadness + emotion.Anger + emotion.Disgust + emotion.Fear;

            return positiveValues - negativeValues;
        }

        private static string GetEmotionType(Emotion emotion)
        {
            string emotionType = string.Empty;
            double emotionValue = 0.0;
            if (emotion.Anger > emotionValue) { emotionValue = emotion.Anger; emotionType = "Anger"; }
            if (emotion.Contempt > emotionValue) { emotionValue = emotion.Contempt; emotionType = "Contempt"; }
            if (emotion.Disgust > emotionValue) { emotionValue = emotion.Disgust; emotionType = "Disgust"; }
            if (emotion.Fear > emotionValue) { emotionValue = emotion.Fear; emotionType = "Fear"; }
            if (emotion.Happiness > emotionValue) { emotionValue = emotion.Happiness; emotionType = "Happiness"; }
            if (emotion.Neutral > emotionValue) { emotionValue = emotion.Neutral; emotionType = "Neutral"; }
            if (emotion.Sadness > emotionValue) { emotionValue = emotion.Sadness; emotionType = "Sadness"; }
            if (emotion.Surprise > emotionValue) { emotionType = "Surprise"; }
            Console.WriteLine($"Emotion : {emotionType}");
            return emotionType;
        }
    }
}