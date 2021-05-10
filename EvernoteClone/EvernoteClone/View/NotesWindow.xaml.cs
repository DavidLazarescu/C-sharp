using Azure.Storage.Blobs;
using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {

        NotesVM vm;


        public NotesWindow()
        {
            InitializeComponent();
            
            //Events
            vm = Resources["vm"] as NotesVM;   //Using resources from the xaml file
            vm.SelectedNoteChanged += VM_SelectedNoteChanged;
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);


            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;
            fontFamilyComboBox.SelectedItem = contentRichTextbox.Selection.GetPropertyValue(Inline.FontFamilyProperty);

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 28, 32, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
            fontSizeComboBox.SelectedItem = contentRichTextbox.Selection.GetPropertyValue(Inline.FontSizeProperty);
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(vm.RenameNotebookTextBoxVisibility == Visibility.Visible)
                    vm.EndRenamingCommand.Execute(vm.SelectedNotebook);
                else if(vm.RenameNoteTextBoxVisibility == Visibility.Visible)
                    vm.EndRenamingCommand.Execute(vm.SelectedNote);
            }
        }

        //Loading in
        private async void VM_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextbox.Document.Blocks.Clear();
            if(vm.SelectedNote != null) 
            {
                if (!string.IsNullOrEmpty(vm.SelectedNote.FileLocation))
                {
                    string downloadPath = System.IO.Path.Combine(Environment.CurrentDirectory, $"{vm.SelectedNote.Id}.rtf");

                    if (File.Exists(downloadPath))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var rtfResult = await client.GetStringAsync(vm.SelectedNote.FileLocation);

                            using (StreamWriter streamWriter = new StreamWriter(downloadPath))
                            {
                                streamWriter.Write(rtfResult);
                            }
                        }

                        using (FileStream fileStream = new FileStream(downloadPath, FileMode.Open))
                        {
                            var contents = new TextRange(contentRichTextbox.Document.ContentStart, contentRichTextbox.Document.ContentEnd);
                            contents.Load(fileStream, DataFormats.Rtf);
                        }
                    }
                }
            }
        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(contentRichTextbox.Document.ContentStart, contentRichTextbox.Document.ContentEnd)).Text.Length - 2;

            statusTextBlock.Text = $"File length: {amountOfCharacters} characters.";
        }


        private async void speechButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isChecked)
            {
                string region = "westeurope";
                string key = "10b3abeb322347b69ae151bb56a618cd";

                var speechConfig = SpeechConfig.FromSubscription(key, region);
                speechConfig.SpeechRecognitionLanguage = "de-DE";
                using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
                {
                    using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                    {
                        var resultText = await recognizer.RecognizeOnceAsync();
                        contentRichTextbox.Document.Blocks.Add(new Paragraph(new Run(resultText.Text)));   //Add a text to the richtTextBlock
                    }
                }
            }
            else
            {

            }
        }



        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            //"IsChecked" is a nullable boolean, the ?? false just says, that the bool should be false if its null
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);  //Makes the selected text Bold
            else
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);  //Makes the selected text Normal
        }

        

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            //"IsChecked" is a nullable boolean, the ?? false just says, that the bool should be false if its null
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);  //Makes the selected text Italic
            else
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);  //Makes the selected text Normal
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            //"IsChecked" is a nullable boolean, the ?? false just says, that the bool should be false if its null
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);  //Makes the selected text to be underlined
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);  //Makes the selected text Normal
            }
        }




        private void contentRichTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextbox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && selectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = contentRichTextbox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = contentRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && selectedDecoration.Equals(TextDecorations.Underline);

            fontFamilyComboBox.SelectedItem = contentRichTextbox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = contentRichTextbox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }



        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
                contentRichTextbox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"{vm.SelectedNote.Id}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
            {
                var content = new TextRange(contentRichTextbox.Document.ContentStart, contentRichTextbox.Document.ContentEnd);
                content.Save(fileStream, DataFormats.Rtf);
            }

            //Uploading the localy saved file
            vm.SelectedNote.FileLocation = await updateFile(rtfFile, fileName);
            await DatabaseHelper.Update(vm.SelectedNote);
        }

        private async Task<string> updateFile(string rtfFilePath, string fileName)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=evernoteclone;AccountKey=B01F+6FCtnu+FnXDMiLIe1zTxG/kvG3e3ne50UK09yexFuW+np5R0ttFen06zlahZv3ELYBfvHsHg+uvKuu17w==;EndpointSuffix=core.windows.net";
            string containerName = "note-storage";

            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(rtfFilePath, overwrite:true);

            return $"https://evernoteclone.blob.core.windows.net/note-storage/{fileName}";
        }
    }
}