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
    /// Interaction logic for ViewProfileWindow.xaml
    /// </summary>
    public partial class ViewProfileWindow : Window
    {
        private readonly IUserService userService;
        private readonly IEducationService educationService;
        private readonly IWorkExperienceService workExperienceService;
        private readonly IMemberSkillService memberSkillService;
        private readonly int _userId;

        public ViewProfileWindow(JobApplication jobApplication)
        {
            InitializeComponent();
            userService = new UserService();
            educationService = new EducationService();
            workExperienceService = new WorkExperienceService();
            memberSkillService = new MemberSkillService();
            _userId = jobApplication.Member.UserId;

            NameTextBox.Text = jobApplication.Member.User.Name;
            if (jobApplication.Member.DateOfBirth != null)
            {
                DateOnly dateOfBirth = DateOnly.FromDateTime((DateTime)jobApplication.Member.DateOfBirth);
                DateOfBirthPicker.Text = dateOfBirth.ToString("dd-MM-yyyy");
            }
            EmailTextBox.Text = jobApplication.Member.User.Email;
            PhoneTextBox.Text = jobApplication.Member.Phone;
            GenderComboBox.Text = jobApplication.Member.Gender == 1 ? "Male" : "Female";

            LoadEducations();
            LoadExperiences();
            LoadMemberSkill();
        }

        public void LoadEducations()
        {
            try
            {
                var educations = educationService.GetEducationsByUserId(_userId);

                if (educations != null)
                {
                    EducationDataGrid.ItemsSource = null;
                    EducationDataGrid.ItemsSource = educations;
                }

            }
            catch
            {
                MessageBox.Show("Error on load educations!");
            }
        }

        public void LoadExperiences()
        {
            try
            {

                var experiences = workExperienceService.GetWorkExperiencesByUserId(_userId);

                if (experiences != null)
                {
                    WorkExperienceDataGrid.ItemsSource = null;
                    WorkExperienceDataGrid.ItemsSource = experiences;
                }

            }
            catch
            {
                MessageBox.Show("Error on load work experiences!");
            }
        }

        public void LoadMemberSkill()
        {
            try
            {

                var memberSkills = memberSkillService.GetMemberSkillsByUserId(_userId);

                if (memberSkills != null)
                {
                    MemberSkillDataGrid.ItemsSource = null;
                    MemberSkillDataGrid.ItemsSource = memberSkills;
                }

            }
            catch
            {
                MessageBox.Show("Error on load member skill!");
            }
        }


    }
}
