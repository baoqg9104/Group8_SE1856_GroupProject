using System;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Word;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using Microsoft.Web.WebView2.Core;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for CVViewerWindow.xaml
    /// </summary>
    public partial class CVViewerWindow : System.Windows.Window
    {
        private string filePath;

        public CVViewerWindow(string cvFilePath)
        {
            InitializeComponent();
            filePath = cvFilePath;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                await webViewer.EnsureCoreWebView2Async();
                LoadDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing viewer: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void LoadDocument()
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                string tempHtmlPath = Path.Combine(Path.GetTempPath(), "temp.html");

                switch (extension)
                {
                    case ".pdf":
                        // Sử dụng built-in PDF viewer của browser
                        webViewer.CoreWebView2.Navigate(filePath);
                        break;

                    case ".doc":
                    case ".docx":
                        ConvertWordToHtml(filePath, tempHtmlPath);
                        webViewer.CoreWebView2.Navigate(tempHtmlPath);
                        break;

                    default:
                        MessageBox.Show("Unsupported file format", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading document: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void ConvertWordToHtml(string wordPath, string htmlPath)
        {
            Microsoft.Office.Interop.Word.Application word = null;
            Document doc = null;

            try
            {
                word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = false;
                doc = word.Documents.Open(wordPath);
                doc.SaveAs2(htmlPath, WdSaveFormat.wdFormatFilteredHTML);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                }
                if (word != null)
                {
                    word.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(word);
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Cleanup temp files
            string tempHtmlPath = Path.Combine(Path.GetTempPath(), "temp.html");
            if (File.Exists(tempHtmlPath))
            {
                try
                {
                    File.Delete(tempHtmlPath);
                }
                catch { /* Ignore cleanup errors */ }
            }
        }
    }
}
