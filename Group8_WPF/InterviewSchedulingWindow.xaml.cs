using BusinessObjects;
using Services;
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
using System.Windows.Shapes;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for InterviewSchedulingWindow.xaml
    /// </summary>
    public partial class InterviewSchedulingWindow : Window
    {
        public event EventHandler InterviewScheduled;
        private readonly JobApplication _jobApplication;
        private readonly IInterviewScheduleService interviewScheduleService;

        public InterviewSchedulingWindow(JobApplication jobApplication)
        {
            InitializeComponent();
            interviewScheduleService = new InterviewScheduleService();
            _jobApplication = jobApplication;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var date = DateSchedule.SelectedDate;
            var time = TimeSchedule.Value;

            if (date == null || !date.HasValue)
            {
                MessageBox.Show("Date is required");
                return;
            }

            if (time == null || !time.HasValue)
            {
                MessageBox.Show("Time is required");
                return;
            }

            var interviewDate = date.Value.Date.Add(time.Value.TimeOfDay);

            try
            {

                var addInterview = new InterviewSchedule()
                {
                    ApplicationId = _jobApplication.ApplicationId,
                    InterviewDate = interviewDate,
                    Status = "Pending"
                };

                interviewScheduleService.AddInterviewSchedule(addInterview);

                InterviewScheduled?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
