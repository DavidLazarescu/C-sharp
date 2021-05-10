﻿using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// "NewtonSoft.Json" Package got added to deserialize JSON strings
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Prediction> resultPredictions;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            //"Image files(*.png; *.jpg)" this gets displayed to the user and "*.png;*.jpg;*jpeg" this is the actual filter
            //which gets readen by the .Filter method
            dialog.Filter = "Image files(*.png; *.jpg)|*.png;*.jpg;*jpeg";  //Filter so only following formats can be selected
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);  //Sets the default dir as local "pictures"

            if (dialog.ShowDialog() == true)
            {
                string fileName = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));

                //MakePredictionAsync(fileName);
            }
        }

        private async void MakePredictionAsync(string fileName)
        {
            string url = "https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/2808d316-362f-4cb3-90e7-629203003648/classify/iterations/Human/image";
            string predictionKey = "45a19daecffc49e18b7950e58ac33ede";
            string contentType = "application/octet-stream";

            var picture = File.ReadAllBytes(fileName);  //Stores the pictures in a byte array

            using (HttpClient client = new HttpClient())
            {
                //Add prediction header
                client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
                
                using(var content = new ByteArrayContent(picture))   //Converts the Byte array to a content
                {
                    //Dont add a usual header for "ContentType", caused by its popularity, it has its own function
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync(); //Gets the response as JSON string

                    //Deserializes the string into objects of the class "CustomVision"
                    //(The classes for this JSON got autogenerated with a online tool: https://jsonutils.com/)
                    resultPredictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).predictions;

                    predictionsListView.ItemsSource = resultPredictions;  //Sets the List View (UI)

                    
                    resultPredictions.Sort((a, b) => b.probability.CompareTo(a.probability)); //Sorts it to the highest possibillity first
                    resultName.Content = resultPredictions.First().tagName;  //Displays the name
                }
            }
        }
    }
}