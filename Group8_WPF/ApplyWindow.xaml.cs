using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using BusinessObjects;
using Services;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for ApplyWindow.xaml
    /// </summary>
    public partial class ApplyWindow : Window
    {
        private string? selectedFilePath;
        private readonly Job _job;
        private readonly IJobApplicationService jobApplicationService;
        private readonly IUserService userService;

        public ApplyWindow(Job job)
        {
            InitializeComponent();
            jobApplicationService = new JobApplicationService();
            userService = new UserService();
            _job = job;
            JobTitle.Text = job.JobTitle + " at " + job.Company.CompanyName;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Documents (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                long fileSizeInBytes = new FileInfo(selectedFilePath).Length;

                if (fileSizeInBytes > 3 * 1024 * 1024)
                {
                    MessageBox.Show("The selected file exceeds the 3MB limit. Please choose a smaller file.", "File Size Limit", MessageBoxButton.OK, MessageBoxImage.Warning);
                    selectedFilePath = null; 
                    FileNameTextBlock.Text = "No file selected";
                }
                else
                {
                    FileNameTextBlock.Text = $"Selected file: {Path.GetFileName(openFileDialog.FileName)}";
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please upload your CV before submitting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string saveDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CVUploads");
            Directory.CreateDirectory(saveDirectory);

            string fileName = Path.GetFileName(selectedFilePath);
            string savedFilePath = Path.Combine(saveDirectory, fileName);


            try
            {
                File.Copy(selectedFilePath, savedFilePath, true);

                SaveApplicationData(savedFilePath);

                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow?.LoadJobApplications();

                MessageBox.Show($"Your application has been submitted, and your CV has been saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                FileNameTextBlock.Text = string.Empty;
                selectedFilePath = null;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save CV file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveApplicationData(string cvFilePath)
        {
            var userId = int.Parse(Application.Current.Properties["UserId"] as string);

            var member = userService.GetUser(userId).Member;

            var jobApplication = new JobApplication()
            {
                JobId = _job.JobId,
                MemberId = member.MemberId,
                ApplicationDate = DateTime.Now,
                Status = "Pending",
                CvFilePath = cvFilePath,

            };

            jobApplicationService.AddJobApplication(jobApplication);
        }
    }
}
