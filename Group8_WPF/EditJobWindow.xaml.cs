using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for EditJobWindow.xaml
    /// </summary>
    public partial class EditJobWindow : Window
    {
        private readonly ISkillService skillService;
        private readonly IJobService jobService;
        private readonly IUserService userService;
        private readonly IJobSkillService jobSkillService;
        private readonly Job _job;

        public EditJobWindow(Job job)
        {
            InitializeComponent();
            skillService = new SkillService();
            jobService = new JobService();
            userService = new UserService();
            jobSkillService = new JobSkillService();
            _job = job;

            var locations = new List<string>() { "Ha Noi", "Ho Chi Minh", "Da Nang", "Others" };
            LocationComboBox.ItemsSource = locations;

            var skills = skillService.GetSkills();
            var skillItems = skills.Select(skill => new SkillItem
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                IsSelected = false
            }).ToList();

            SkillListBox.ItemsSource = skillItems;

            JobTitleTextBox.Text = job.JobTitle;
            DescriptionTextBox.Text = job.JobDescription;
            SalaryTextBox.Text = job.Salary.ToString();
            LocationComboBox.Text = job.Location;
            CurrencyComboBox.Text = job.CurrencyCode;
            DeadlineDatePicker.SelectedDate = job.ApplicationDeadline;

            var jobSkills = job.JobSkills.Select(js => js.SkillId).ToList();

            foreach (var item in SkillListBox.Items.Cast<SkillItem>())
            {
                item.IsSelected = jobSkills.Contains(item.SkillId);
            }

            SkillListBox.Items.Refresh();
            UpdateSelectedSkillsText();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var jobTitle = JobTitleTextBox.Text;
                var desc = DescriptionTextBox.Text;
                var salaryText = SalaryTextBox.Text;
                var location = LocationComboBox.Text;
                var deadline = DeadlineDatePicker.SelectedDate;
                var currency = CurrencyComboBox.Text;

                if (string.IsNullOrWhiteSpace(jobTitle))
                {
                    throw new Exception("Job Title is required");
                }

                if (string.IsNullOrWhiteSpace(desc))
                {
                    throw new Exception("Description is required");
                }

                if (string.IsNullOrWhiteSpace(salaryText))
                {
                    throw new Exception("Salary is required");
                }

                if (!double.TryParse(salaryText, out double salary) || salary <= 0)
                {
                    throw new Exception("Salary must be a positive number");
                }

                if (string.IsNullOrWhiteSpace(currency))
                {
                    throw new Exception("Currency is required");
                }

                if (string.IsNullOrWhiteSpace(location))
                {
                    throw new Exception("Location is required");
                }

                if (!deadline.HasValue)
                {
                    throw new Exception("Deadline is required");
                }
                else if (deadline.Value <= DateTime.Now)
                {
                    throw new Exception("Application deadline must be in the future");
                }

                var selectedSkills = SkillListBox.Items
                .Cast<SkillItem>()
                .Where(item => item.IsSelected)
                .ToList();

                if (selectedSkills.IsNullOrEmpty())
                {
                    throw new Exception("Skills are required");
                }

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);
                var companyId = userService.GetUser(userId).CompanyId;

                var updateJob = new Job()
                {
                    JobId = _job.JobId,
                    CompanyId = (int)companyId,
                    JobTitle = jobTitle,
                    JobDescription = desc,
                    Location = location,
                    Salary = salary,
                    ApplicationDeadline = (DateTime)deadline,
                    Status = true,
                    CurrencyCode = currency,
                    JobSkills = _job.JobSkills,
                };

                AddSkillsToJob(updateJob);

                jobService.UpdateJob(updateJob);

                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow?.LoadManageJob();

                MessageBox.Show("Update job successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddSkillsToJob(Job job)
        {
            var selectedSkillIds = SkillListBox.Items
            .Cast<SkillItem>()
            .Where(item => item.IsSelected)
            .Select(item => item.SkillId)
            .ToList();

            var existingSkills = jobSkillService
                .GetJobSkills()
                .Where(jk => jk.JobId == job.JobId);

            foreach (var skill in existingSkills)
            {
                jobSkillService.RemoveJobSkill(skill); 
            }

            foreach (var skillId in selectedSkillIds)
            {
                var jobSkill = new JobSkill
                {
                    SkillId = skillId,
                    JobId = job.JobId
                };
                jobSkillService.AddJobSkill(jobSkill);
            }
        }

        private void SkillChecked(object sender, RoutedEventArgs e)
        {
            UpdateSelectedSkillsText();
        }

        private void SkillUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateSelectedSkillsText();
        }

        private void UpdateSelectedSkillsText()
        {
            var selectedSkills = SkillListBox.Items
                .Cast<SkillItem>()
                .Where(item => item.IsSelected)
                .Select(item => item.SkillName)
                .ToList();

            var selectedSkillsText = string.Join(", ", selectedSkills);

            SelectedSkillsTextBlock.Text = selectedSkillsText;
        }
    }
}
