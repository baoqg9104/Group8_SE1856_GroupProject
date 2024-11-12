using BusinessObjects;
using DataAccessLayers.DTOs.Member;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IJobService jobService;
        private readonly IRecruitmentCompanyService companyService;
        private readonly IUserService userService;
        private readonly IMemberService memberService;
        private readonly IEducationService educationService;
        private readonly IWorkExperienceService workExperienceService;
        private readonly IMemberSkillService memberSkillService;
        private readonly IJobApplicationService jobApplicationService;
        private readonly IInterviewScheduleService interviewScheduleService;
        private readonly ISkillService skillService;

        public string CurrentUserRole { get; set; }
        public string CurrentUsername { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            jobService = new JobService();
            companyService = new RecruitmentCompanyService();
            userService = new UserService();
            memberService = new MemberService();
            educationService = new EducationService();
            workExperienceService = new WorkExperienceService();
            memberSkillService = new MemberSkillService();
            jobApplicationService = new JobApplicationService();
            interviewScheduleService = new InterviewScheduleService();
            skillService = new SkillService();

            CurrentUserRole = "Guest";
            UpdateUIForRole(CurrentUserRole, "");
            ViewCompanyBtn.IsEnabled = false;
        }
        public void UpdateUIForRole(string role, string name)
        {
            Jobs.Visibility = Visibility.Collapsed;
            Companies.Visibility = Visibility.Collapsed;
            MyProfile.Visibility = Visibility.Collapsed;
            SignupBtn.Visibility = Visibility.Collapsed;
            JobApplied.Visibility = Visibility.Collapsed;
            InterviewSchedule.Visibility = Visibility.Collapsed;
            ApplyBtn.Visibility = Visibility.Collapsed;
            WithdrawBtn.Visibility = Visibility.Collapsed;
            ManageJob.Visibility = Visibility.Collapsed;
            ManageApplicants.Visibility = Visibility.Collapsed;
            InterviewScheduleCompany.Visibility = Visibility.Collapsed;
            CompanyProfile.Visibility = Visibility.Collapsed;
            ManageMember.Visibility = Visibility.Collapsed;

            if (role == "Guest")
            {
                LoadJobs();
                LoadCompanies();
                Jobs.Visibility = Visibility.Visible;
                Companies.Visibility = Visibility.Visible;
                SignupBtn.Visibility = Visibility.Visible;
                LoginBtn.Content = "Login";
                LoginBtn.Click -= Logout_Click;
                LoginBtn.Click += Login_Click;
            }
            else if (role == "Member")
            {
                LoadJobs();
                LoadCompanies();
                LoadProfile();
                LoadEducations();
                LoadExperiences();
                LoadMemberSkill();
                LoadJobApplications();
                LoadInterviewSchedule();
                Jobs.Visibility = Visibility.Visible;
                Companies.Visibility = Visibility.Visible;
                JobApplied.Visibility = Visibility.Visible;
                InterviewSchedule.Visibility = Visibility.Visible;
                MyProfile.Visibility = Visibility.Visible;
                SignupBtn.Visibility = Visibility.Collapsed;
                LoginBtn.Content = "Log out";
                LoginBtn.Click -= Login_Click;
                LoginBtn.Click += Logout_Click;
                ApplyBtn.Visibility = Visibility.Visible;
                ApplyBtn.IsEnabled = false;
                WithdrawBtn.Visibility = Visibility.Visible;
                WithdrawBtn.IsEnabled = false;
                ConfirmBtn.IsEnabled = false;
                CancelBtn.IsEnabled = false;
            }
            else if (role == "Company")
            {
                LoadManageJob();
                LoadManageApplicant();
                LoadManageInterview();
                ManageJob.Visibility = Visibility.Visible;
                ManageApplicants.Visibility = Visibility.Visible;
                InterviewScheduleCompany.Visibility = Visibility.Visible;
                CompanyProfile.Visibility = Visibility.Visible;
                SignupBtn.Visibility = Visibility.Collapsed;
                LoginBtn.Content = "Log out";
                LoginBtn.Click -= Login_Click;
                LoginBtn.Click += Logout_Click;

                ViewProfileBtn.IsEnabled = false;
                AcceptBtn.IsEnabled = false;
                RejectBtn.IsEnabled = false;
                OnHoldBtn.IsEnabled = false;
                CompanyCompleteBtn.IsEnabled = false;
                CompanyNoShowBtn.IsEnabled = false;

                var locations = new List<string>() { "Ha Noi", "Ho Chi Minh", "Da Nang" };
                LocationComboBox.ItemsSource = locations;

                var skills = skillService.GetSkills();
                var skillItems = skills.Select(skill => new SkillItem
                {
                    SkillId = skill.SkillId,
                    SkillName = skill.SkillName,
                    IsSelected = false
                }).ToList();

                SkillListBox.ItemsSource = skillItems;

                MainTabControl.SelectedIndex = 5;
            }
            else if (role == "Admin")
            {
                ManageMember.Visibility = Visibility;
                SignupBtn.Visibility = Visibility.Collapsed;
                UpdateMemberBtn.IsEnabled = false;
                DeleteMemberBtn.IsEnabled = false;
                ActivateMemberBtn.IsEnabled = false;
                BanMemberBtn.IsEnabled = false;

                LoginBtn.Content = "Log out";
                LoginBtn.Click -= Login_Click;
                LoginBtn.Click += Logout_Click;
                MainTabControl.SelectedIndex = 9;
            }

            if (role == "Guest")
            {
                Username.Text = $"- Guest";
            }
            else
            {
                Username.Text = $"- {name} - {role}";
            }
        }

        public void SetUser(string role, string name)
        {
            CurrentUserRole = role;
            CurrentUsername = name;
            UpdateUIForRole(role, name);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadJobs();
            LoadCompanies();
        }

        private void LoadJobs()
        {
            try
            {
                var jobs = jobService.GetJobs();
                if (jobs != null)
                {
                    JobDataGrid.ItemsSource = null;
                    JobDataGrid.ItemsSource = jobs
                        .Where(j => j.Status && j.ApplicationDeadline >= DateTime.Now.Date)
                        .OrderBy(j => j.ApplicationDeadline);
                }
            }
            catch
            {
                MessageBox.Show("Error on load list of customer!");
            }
        }

        private void LoadCompanies()
        {
            try
            {
                var companies = companyService.GetRecruitmentCompanies();
                if (companies != null)
                {
                    CompanyDataGrid.ItemsSource = null;
                    CompanyDataGrid.ItemsSource = companies;
                }
            }
            catch
            {
                MessageBox.Show("Error on load list of companies!");
            }
        }

        private void LoadProfile()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var name = user.Name;
                var email = user.Email;
                var password = user.Password;
                var dateOfBirth = user.Member.DateOfBirth;
                var phone = user.Member.Phone;
                var gender = user.Member.Gender;

                NameTextBox.Text = name;
                EmailTextBox.Text = email;
                PasswordBox.Password = password;
                DateOfBirthPicker.SelectedDate = dateOfBirth;
                PhoneTextBox.Text = phone;

                if (gender != null)
                {
                    GenderComboBox.SelectedIndex = (int)gender == 0 ? 1 : 0;
                }
            }
            catch
            {
                MessageBox.Show("Error on load profile!");
            }
        }

        public void LoadEducations()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var educations = educationService.GetEducationsByUserId(userId);

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
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var experiences = workExperienceService.GetWorkExperiencesByUserId(userId);

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
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var memberSkills = memberSkillService.GetMemberSkillsByUserId(userId);

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

        public void LoadJobApplications()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var memberId = user.Member.MemberId;

                var jobApplications = jobApplicationService
                    .GetJobApplications()
                    .Where(ja => ja.MemberId == memberId)
                    .ToList();

                if (jobApplications != null)
                {
                    JobAppliedDataGrid.ItemsSource = null;
                    JobAppliedDataGrid.ItemsSource = jobApplications;
                }

            }
            catch
            {
                MessageBox.Show("Error on load job application!");
            }
        }

        public void LoadInterviewSchedule()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var memberId = user.Member.MemberId;

                var interviewSchedules = interviewScheduleService
                    .GetInterviewSchedules()
                    .Where(i => i.JobApplication.MemberId == memberId)
                    .ToList();

                if (interviewSchedules != null)
                {
                    InterviewDataGrid.ItemsSource = null;
                    InterviewDataGrid.ItemsSource = interviewSchedules;
                }
            }
            catch
            {
                MessageBox.Show("Error on load interview schedule!");
            }
        }

        public void LoadManageJob()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var companyId = user.CompanyId;

                var companyJobs = jobService
                    .GetJobs()
                    .Where(j => j.CompanyId == companyId && j.Status == true)
                    .OrderBy(j => j.ApplicationDeadline)
                    .ToList();

                if (companyJobs != null)
                {
                    ManageJobDataGrid.ItemsSource = null;
                    ManageJobDataGrid.ItemsSource = companyJobs;
                }
            }
            catch
            {
                MessageBox.Show("Error on load manage job!");
            }
        }

        public void LoadManageApplicant()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var companyId = user.CompanyId;

                var jobApplications = jobApplicationService
                    .GetJobApplications()
                    .Where(ja => ja.Job.CompanyId == companyId &&
                    ja.Status != "Job Deleted" &&
                    ja.Status != "Withdrawn")
                    .OrderBy(ja => ja.JobId)
                    .ToList();

                if (jobApplications != null)
                {
                    ManageApplicantDataGrid.ItemsSource = null;
                    ManageApplicantDataGrid.ItemsSource = jobApplications;
                    LoadAndGroupData();
                }

            }
            catch
            {
                MessageBox.Show("Error on load manage applicant!");
            }
        }

        private void LoadAndGroupData()
        {
            var view = CollectionViewSource.GetDefaultView(ManageApplicantDataGrid.ItemsSource);

            // Clear any existing grouping
            view.GroupDescriptions.Clear();

            // Group by JobId (this will automatically group items with same JobId and JobTitle together)
            view.GroupDescriptions.Add(new PropertyGroupDescription("Job.JobId"));

            // Sort by JobId
            view.SortDescriptions.Add(new SortDescription("Job.JobId", ListSortDirection.Ascending));
        }

        public void LoadManageInterview()
        {
            try
            {
                CompanyCompleteBtn.IsEnabled = false;

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var companyId = user.CompanyId;

                var interviewSchedules = interviewScheduleService
                    .GetInterviewSchedules()
                    .Where(i => i.JobApplication.Job.CompanyId == companyId)
                    .OrderBy(i => i.JobApplication.JobId)
                    .ToList();

                if (interviewSchedules != null)
                {
                    ManageInterviewDataGrid.ItemsSource = null;
                    ManageInterviewDataGrid.ItemsSource = interviewSchedules;
                }
            }
            catch
            {
                MessageBox.Show("Error on load manage interview!");
            }
        }

        public void LoadCompanyProfile()
        {
            try
            {
                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var companyId = user.CompanyId;

                var company = companyService.GetRecruitmentCompany((int)companyId);

                var countries = new List<string>() { "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua and Barbuda", "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Central African Republic", "Chad", "Chile", "China", "Colombia", "Comoros", "Congo", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, North", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Macedonia", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russia", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };

                CountryTextBox.ItemsSource = countries;

                AccCompanyNameTextBox.Text = user.Name;
                AccCompanyEmailTextBox.Text = user.Email;
                AccCompanyPasswordBox.Password = user.Password;

                CompanyNameTextBox.Text = company.CompanyName;
                CountryTextBox.Text = company.Country;
                CompanyDescriptionTextBox.Text = company.Description;
                IndustryTextBox.Text = company.Industry;
                CompanyTypeTextBox.Text = company.CompanyType;
            }
            catch
            {
                MessageBox.Show("Error on load company profile!");
            }
        }

        public void LoadManageMember()
        {
            try
            {
                var members = memberService
                    .GetMembers()
                    .Where(x => x.User.Status != UserStatus.Deleted)
                    .ToList();

                if (members != null)
                {
                    ManageMemberDataGrid.ItemsSource = null;
                    ManageMemberDataGrid.ItemsSource = members;
                }
            }
            catch
            {
                MessageBox.Show("Error on load manage member!");
            }
        }

        private void JobDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JobDataGrid.SelectedItem != null)
            {
                ViewCompanyBtn.IsEnabled = true;
                ApplyBtn.IsEnabled = true;
            }
            else
            {
                ViewCompanyBtn.IsEnabled = false;
                ApplyBtn.IsEnabled = false;
            }
        }

        private void JobAppliedGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var jobApplication = JobAppliedDataGrid.SelectedItem as JobApplication;

            if (jobApplication == null)
            {
                WithdrawBtn.IsEnabled = false;
                return;
            }

            if (jobApplication.Status == "Pending")
            {
                WithdrawBtn.IsEnabled = true;
                return;
            }

            WithdrawBtn.IsEnabled = false;
        }

        private void InterviewDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var interview = InterviewDataGrid.SelectedItem as InterviewSchedule;

            if (interview == null)
            {
                ConfirmBtn.IsEnabled = false;
                CancelBtn.IsEnabled = false;
                return;
            }

            if (interview.Status == "Pending")
            {
                ConfirmBtn.IsEnabled = true;
                CancelBtn.IsEnabled = true;
            }
            else if (interview.Status == "Confirmed")
            {
                CancelBtn.IsEnabled = true;
            }

        }

        private void ManageApplicantDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManageApplicantDataGrid.SelectedItem != null)
            {
                ViewProfileBtn.IsEnabled = true;

                var jobA = ManageApplicantDataGrid.SelectedItem as JobApplication;

                AcceptBtn.IsEnabled = false;
                RejectBtn.IsEnabled = false;
                OnHoldBtn.IsEnabled = false;

                if (jobApplicationService.GetJobApplications().Any(ja => ja.Status == "Accepted" && ja.JobId == jobA.JobId))
                {
                    return;
                }

                if (jobA.Status == "Pending")
                {
                    AcceptBtn.IsEnabled = true;
                    RejectBtn.IsEnabled = true;
                    OnHoldBtn.IsEnabled = true;
                }
                else
                if (jobA.Status == "On Hold")
                {
                    AcceptBtn.IsEnabled = true;
                    RejectBtn.IsEnabled = true;
                }
            }
            else
            {
                ViewProfileBtn.IsEnabled = false;
                AcceptBtn.IsEnabled = false;
                RejectBtn.IsEnabled = false;
                OnHoldBtn.IsEnabled = false;
            }
        }

        private void ManageMemberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var member = ManageMemberDataGrid.SelectedItem as Member;
            if (member == null)
            {
                return;
            }

            UpdateMemberBtn.IsEnabled = true;
            DeleteMemberBtn.IsEnabled = true;
            ActivateMemberBtn.IsEnabled = false;
            BanMemberBtn.IsEnabled = false;

            if (member.User.Status == UserStatus.Active)
            {
                BanMemberBtn.IsEnabled = true;
            }
            else if (member.User.Status == UserStatus.Banned)
            {
                ActivateMemberBtn.IsEnabled = true;
            }

        }

        private void SkillChecked(object sender, RoutedEventArgs e)
        {
            // Khi một CheckBox được chọn, cập nhật chuỗi kỹ năng đã chọn
            UpdateSelectedSkillsText();
        }

        private void SkillUnchecked(object sender, RoutedEventArgs e)
        {
            // Khi một CheckBox bị bỏ chọn, cập nhật chuỗi kỹ năng đã chọn
            UpdateSelectedSkillsText();
        }

        private void UpdateSelectedSkillsText()
        {
            // Lấy tất cả các kỹ năng đã chọn từ ListBox
            var selectedSkills = SkillListBox.Items
                .Cast<SkillItem>()
                .Where(item => item.IsSelected)  // Lọc những mục đã được chọn
                .Select(item => item.SkillName)   // Lấy tên kỹ năng
                .ToList();

            // Kết hợp các kỹ năng đã chọn thành chuỗi, mỗi kỹ năng cách nhau bằng dấu phẩy
            var selectedSkillsText = string.Join(", ", selectedSkills);

            // Cập nhật TextBlock với chuỗi kỹ năng đã chọn
            SelectedSkillsTextBlock.Text = selectedSkillsText;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window is LoginWindow)
            //    {
            //        return;
            //    }
            //}

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }


        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window is SignupWindow)
            //    {
            //        //window.Activate();
            //        return;
            //    }
            //}

            SignupWindow signupWindow = new SignupWindow();
            signupWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties.Remove("UserId");
            SetUser("Guest", "");
            MainTabControl.SelectedIndex = 0;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchBox.Text.ToLower().Trim();
            var selectedCity = CityBox.Text.ToLower();

            var excludedCities = new[] { "ho chi minh", "ha noi", "da nang" };

            var filteredJobs = jobService.GetJobs()
                .Where(j => j.Status == true &&
                            (j.Company.CompanyName.ToLower().Contains(searchTerm) ||
                             j.JobTitle.ToLower().Contains(searchTerm) ||
                             j.JobDescription.ToLower().Contains(searchTerm) ||
                             j.JobSkills.Any(js => js.Skill.SkillName.ToLower().Contains(searchTerm))
                             )
                           )
                .Where(j =>
                    j.Location.ToLower() == selectedCity ||
                    selectedCity == "all cities" ||
                    (selectedCity == "others" && !excludedCities.Contains(j.Location.ToLower()))
                )
                .Where(j => j.ApplicationDeadline >= DateTime.Now.Date)
                .OrderBy(j => j.ApplicationDeadline)
                .ToList();

            JobDataGrid.ItemsSource = filteredJobs;
        }


        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search_Click(sender, e);
            }
        }

        private void ViewCompany_Click(object sender, RoutedEventArgs e)
        {
            if (JobDataGrid.SelectedItem != null)
            {
                var job = JobDataGrid.SelectedItem as Job;
                var company = companyService.GetRecruitmentCompany(job.CompanyId);


                if (company != null)
                {
                    // Create and show the CompanyInfoWindow with the company details
                    var companyInfoWindow = new CompanyInfoWindow(company);

                    companyInfoWindow.Show(); // Show as a modal dialog
                }
                else
                {
                    MessageBox.Show("Company not found!");
                }

            }
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchCompanyBox.Text.ToLower().Trim();
            var selectedCountry = CountyBox.Text.ToLower();

            var countriesToExclude = new[] { "vietnam", "japan", "singapore", "australia", "south korea" };

            var filteredCompanies = companyService.GetRecruitmentCompanies()
                .Where(company =>
                    company.CompanyName.ToLower().Contains(searchTerm) ||
                    company.CompanyType.ToLower().Contains(searchTerm) ||
                    company.Description.ToLower().Contains(searchTerm) ||
                    company.Industry.ToLower().Contains(searchTerm))
                .Where(company =>
                    company.Country.ToLower() == selectedCountry ||
                    selectedCountry == "all countries" ||
                    (selectedCountry == "others" && !countriesToExclude.Contains(company.Country.ToLower()))
                );

            CompanyDataGrid.ItemsSource = filteredCompanies.ToList();
        }


        private void SearchCompanyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchCompany_Click(sender, e);
            }
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = NameTextBox.Text;
                var email = EmailTextBox.Text;
                var password = PasswordBox.Password;
                var dateOfBirth = DateOfBirthPicker.SelectedDate;
                var phone = PhoneTextBox.Text;
                var gender = GenderComboBox.SelectedIndex;

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var updateMember = new UpdateMemberDTO
                {
                    UserId = userId,
                    Name = name,
                    Email = email,
                    Password = password,
                    DateOfBirth = dateOfBirth,
                    Phone = phone,
                    Gender = gender,
                };

                memberService.UpdateMember(updateMember);
                MessageBox.Show("Save profile successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddEducation_Click(object sender, RoutedEventArgs e)
        {
            AddEducationWindow addEducationWindow = new AddEducationWindow();
            addEducationWindow.ShowDialog();
        }

        private void EditEducation_Click(object sender, RoutedEventArgs e)
        {
            var education = EducationDataGrid.SelectedItem as Education;
            if (education == null)
            {
                return;
            }

            EditEducationWindow editEducationWindow = new EditEducationWindow(education);
            editEducationWindow.ShowDialog();
        }

        private void DeleteEducation_Click(object sender, RoutedEventArgs e)
        {
            var education = EducationDataGrid.SelectedItem as Education;
            if (education == null)
            {
                return;
            }

            var result = MessageBox.Show("Delete this education?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                educationService.DeleteEducation(education);
                LoadEducations();
            }
        }

        private void AddWorkExperience_Click(object sender, RoutedEventArgs e)
        {
            AddWorkExperienceWindow addWorkExperienceWindow = new AddWorkExperienceWindow();
            addWorkExperienceWindow.ShowDialog();
        }

        private void EditWorkExperience_Click(object sender, RoutedEventArgs e)
        {
            var experience = WorkExperienceDataGrid.SelectedItem as WorkExperience;
            if (experience == null)
            {
                return;
            }

            EditWorkExperienceWindow editWorkExperienceWindow = new EditWorkExperienceWindow(experience);
            editWorkExperienceWindow.ShowDialog();
        }

        private void DeleteWorkExperience_Click(object sender, RoutedEventArgs e)
        {
            var experience = WorkExperienceDataGrid.SelectedItem as WorkExperience;
            if (experience == null)
            {
                return;
            }

            var result = MessageBox.Show("Delete this work experience?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                workExperienceService.DeleteWorkExperience(experience);
                LoadExperiences();
            }
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            AddMemberSkillWindow addSkillWindow = new AddMemberSkillWindow();
            addSkillWindow.ShowDialog();
        }

        private void EditSkill_Click(object sender, RoutedEventArgs e)
        {
            var memberSkill = MemberSkillDataGrid.SelectedItem as MemberSkill;
            if (memberSkill == null)
            {
                return;
            }

            EditMemberSkillWindow editMemberSkillWindow = new EditMemberSkillWindow(memberSkill);
            editMemberSkillWindow.ShowDialog();
        }

        private void DeleteSkill_Click(object sender, RoutedEventArgs e)
        {
            var memberSkill = MemberSkillDataGrid.SelectedItem as MemberSkill;
            if (memberSkill == null)
            {
                return;
            }

            var result = MessageBox.Show("Delete this skill?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                memberSkillService.DeleteMemberSkill(memberSkill);
                LoadMemberSkill();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            var job = JobDataGrid.SelectedItem as Job;

            if (job == null)
            {
                return;
            }

            ApplyWindow applyWindow = new ApplyWindow(job);
            applyWindow.ShowDialog();
        }

        // Job Applied

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            var jobApplication = JobAppliedDataGrid.SelectedItem as JobApplication;

            if (jobApplication == null)
            {
                return;
            }

            if (jobApplication.Status != "Pending")
            {
                MessageBox.Show("You cannot withdraw this application.");
                return;
            }

            var result = MessageBox.Show("Withdraw this application?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                jobApplication.Status = "Withdrawn";
                jobApplicationService.UpdateJobApplication(jobApplication);
                LoadJobApplications();
            }

        }

        private void ViewCVButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string cvFilePath = button.Tag.ToString();

            if (File.Exists(cvFilePath))
            {
                var viewerWindow = new CVViewerWindow(cvFilePath);
                viewerWindow.Show();
            }
            else
            {
                MessageBox.Show("CV file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Recruitment Company
        // Manage Job
        private void AddJob_Click(object sender, RoutedEventArgs e)
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

                var addJob = new Job()
                {
                    CompanyId = (int)companyId,
                    JobTitle = jobTitle,
                    JobDescription = desc,
                    Location = location,
                    Salary = salary,
                    ApplicationDeadline = (DateTime)deadline,
                    Status = true,
                    CurrencyCode = currency,
                };

                AddSkillsToJob(addJob);

                jobService.AddJob(addJob);

                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow?.LoadManageJob();

                MessageBox.Show("Add job successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void AddSkillsToJob(Job job)
        {
            var selectedSkills = SkillListBox.Items
                .Cast<SkillItem>()
                .Where(item => item.IsSelected)
                .ToList();

            job.JobSkills = new List<JobSkill>();

            foreach (var skill in selectedSkills)
            {
                job.JobSkills.Add(new JobSkill
                {
                    SkillId = skill.SkillId,
                    JobId = job.JobId
                });
            }
        }

        private void EditJob_Click(object sender, RoutedEventArgs e)
        {
            var job = ManageJobDataGrid.SelectedItem as Job;
            if (job == null)
            {
                return;
            }

            EditJobWindow editJobWindow = new EditJobWindow(job);
            editJobWindow.ShowDialog();
        }

        private void DeleteJob_Click(object sender, RoutedEventArgs e)
        {
            var job = ManageJobDataGrid.SelectedItem as Job;
            if (job == null)
            {
                return;
            }

            var result = MessageBox.Show("Delete this job?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                jobService.DeleteJob(job);
                LoadManageJob();
            }
        }

        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            var jobApplication = ManageApplicantDataGrid.SelectedItem as JobApplication;
            if (jobApplication == null)
            {
                return;
            }

            ViewProfileWindow viewProfileWindow = new ViewProfileWindow(jobApplication);
            viewProfileWindow.ShowDialog();

        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var jobApplication = ManageApplicantDataGrid.SelectedItem as JobApplication;
            if (jobApplication == null)
            {
                return;
            }

            InterviewSchedulingWindow interviewSchedulingWindow = new InterviewSchedulingWindow(jobApplication);

            interviewSchedulingWindow.InterviewScheduled += (s, args) =>
            {
                jobApplication.Status = "Accepted";
                jobApplicationService.UpdateJobApplication(jobApplication);

                var list = jobApplicationService
                    .GetJobApplications()
                    .Where(ja => ja.JobId == jobApplication.JobId &&
                    ja.MemberId != jobApplication.MemberId &&
                    ja.Status == "Pending")
                    .ToList();

                foreach (var jobA in list)
                {
                    jobA.Status = "Rejected";
                    jobApplicationService.UpdateJobApplication(jobA);
                }

                LoadManageApplicant();
            };


            interviewSchedulingWindow.ShowDialog();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            var jobApplication = ManageApplicantDataGrid.SelectedItem as JobApplication;
            if (jobApplication == null)
            {
                return;
            }

            var result = MessageBox.Show("Reject this applicant?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                jobApplication.Status = "Rejected";
                jobApplicationService.UpdateJobApplication(jobApplication);
                LoadManageApplicant();
            }
        }

        private void OnHold_Click(object sender, RoutedEventArgs e)
        {
            var jobApplication = ManageApplicantDataGrid.SelectedItem as JobApplication;
            if (jobApplication == null)
            {
                return;
            }

            var result = MessageBox.Show("On Hold this applicant?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                jobApplication.Status = "On Hold";
                jobApplicationService.UpdateJobApplication(jobApplication);
                LoadManageApplicant();
            }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is TabControl)
            {
                var tabControl = sender as TabControl;
                var selectedTabItem = tabControl?.SelectedItem as TabItem;

                if (selectedTabItem != null)
                {
                    if (selectedTabItem.Header.Equals("Jobs"))
                    {
                        LoadJobs();
                    }
                    else if (selectedTabItem.Header.Equals("Companies"))
                    {
                        LoadCompanies();
                    }
                    else if (selectedTabItem.Header.Equals("Job Applied"))
                    {
                        LoadJobApplications();
                    }
                    else if (selectedTabItem.Header.Equals("Interview Schedule"))
                    {
                        LoadInterviewSchedule();
                    }
                    else if (selectedTabItem.Header.Equals("My Profile"))
                    {
                        LoadProfile();
                    }
                    else
                    if (selectedTabItem.Header.Equals("Manage Job"))
                    {
                        LoadManageJob();
                    }
                    else if (selectedTabItem.Header.Equals("Manage Applicants"))
                    {
                        LoadManageApplicant();
                    }
                    else if (selectedTabItem.Header.Equals("Manage Interview"))
                    {
                        LoadManageInterview();
                    }
                    else if (selectedTabItem.Header.Equals("Company Profile"))
                    {
                        LoadCompanyProfile();
                    }
                    else if (selectedTabItem.Header.Equals("Manage Member"))
                    {
                        LoadManageMember();
                    }
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var interview = InterviewDataGrid.SelectedItem as InterviewSchedule;
            if (interview == null)
            {
                return;
            }

            try
            {
                var result = MessageBox.Show("Confirm this interview?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    interview.Status = "Confirmed";
                    interviewScheduleService.UpdateInterviewSchedule(interview);
                    LoadInterviewSchedule();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            var interview = ManageInterviewDataGrid.SelectedItem as InterviewSchedule;
            if (interview == null)
            {
                return;
            }

            try
            {
                var result = MessageBox.Show("Complete this interview?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    interview.Status = "Completed";
                    interviewScheduleService.UpdateInterviewSchedule(interview);
                    LoadManageInterview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NoShow_Click(object sender, RoutedEventArgs e)
        {
            var interview = ManageInterviewDataGrid.SelectedItem as InterviewSchedule;
            if (interview == null)
            {
                return;
            }

            try
            {
                var result = MessageBox.Show("Confirm this applicant is not attend?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    interview.Status = "No Show";
                    interviewScheduleService.UpdateInterviewSchedule(interview);
                    LoadManageInterview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveAccCompanyProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = AccCompanyNameTextBox.Text;
                var password = AccCompanyPasswordBox.Password;
                var email = AccCompanyEmailTextBox.Text;

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var updateUser = new User
                {
                    UserId = userId,
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = userService.GetUser(userId).RoleId,
                    CompanyId = userService.GetUser(userId).CompanyId
                };

                userService.UpdateUser(updateUser);
                MessageBox.Show("Save profile successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SaveCompanyProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyName = AccCompanyNameTextBox.Text;
                var country = CountryTextBox.Text;
                var industry = IndustryTextBox.Text;
                var type = CompanyTypeTextBox.Text;
                var desc = CompanyDescriptionTextBox.Text;

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var user = userService.GetUser(userId);

                var company = companyService.GetRecruitmentCompany((int)user.CompanyId);

                var updateCompany = new RecruitmentCompany()
                {
                    CompanyId = company.CompanyId,
                    Industry = company.Industry,
                    Country = company.Country,
                    Description = company.Description,
                    CompanyName = company.CompanyName,
                    CompanyType = company.CompanyType
                };

                companyService.UpdateRecruitmentCompany(updateCompany);
                MessageBox.Show("Save company information successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CompanyInterviewDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var interview = ManageInterviewDataGrid.SelectedItem as InterviewSchedule;
            if (interview == null)
            {
                CompanyCompleteBtn.IsEnabled = false;
                CompanyNoShowBtn.IsEnabled = false;
                return;
            }

            if (interview.Status == "Confirmed")
            {
                CompanyCompleteBtn.IsEnabled = true;
                CompanyNoShowBtn.IsEnabled = true;

            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var interview = InterviewDataGrid.SelectedItem as InterviewSchedule;
            if (interview == null)
            {
                return;
            }

            try
            {
                var result = MessageBox.Show("Cancel this interview?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    interview.Status = "Canceled";
                    interviewScheduleService.UpdateInterviewSchedule(interview);
                    LoadInterviewSchedule();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Manage member
        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            AddMemberWindow addMemberWindow = new AddMemberWindow();
            addMemberWindow.ShowDialog();
            LoadManageMember();

        }

        private void ActiveMember_Click(object sender, RoutedEventArgs e)
        {
            var member = ManageMemberDataGrid.SelectedItem as Member;

            if (member == null)
            {
                return;
            }

            var user = userService.GetUser(member.UserId);
            try
            {
                var result = MessageBox.Show("Active this account?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    user.Status = UserStatus.Active;
                    userService.UpdateUser(user);

                    var jobApplieds = jobApplicationService
                        .GetJobApplications()
                        .Where(x => x.MemberId == member.MemberId);

                    foreach (var application in jobApplieds)
                    {
                        application.Status = "Canceled";
                        jobApplicationService.UpdateJobApplication(application);
                    }

                    var interviews = interviewScheduleService
                        .GetInterviewSchedules()
                        .Where(x => jobApplieds.Select(y => y.ApplicationId).Contains(x.ApplicationId));

                    foreach (var i in interviews)
                    {
                        i.Status = "Canceled";
                        interviewScheduleService.UpdateInterviewSchedule(i, "Active");
                    }

                    LoadManageMember();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BanMember_Click(object sender, RoutedEventArgs e)
        {
            var member = ManageMemberDataGrid.SelectedItem as Member;

            if (member == null)
            {
                return;
            }

            var user = userService.GetUser(member.UserId);
            try
            {
                var result = MessageBox.Show("Ban this account?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    user.Status = UserStatus.Banned;
                    userService.UpdateUser(user);

                    var jobApplieds = jobApplicationService
                        .GetJobApplications()
                        .Where(x => x.MemberId == member.MemberId);

                    foreach (var application in jobApplieds)
                    {
                        application.Status = "Member Banned";
                        jobApplicationService.UpdateJobApplication(application);
                    }

                    var interviews = interviewScheduleService
                        .GetInterviewSchedules()
                        .Where(x => jobApplieds.Select(y => y.ApplicationId).Contains(x.ApplicationId));

                    foreach (var i in interviews)
                    {
                        i.Status = "Member Banned";
                        interviewScheduleService.UpdateInterviewSchedule(i);
                    }

                    LoadManageMember();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            var member = ManageMemberDataGrid.SelectedItem as Member;

            if (member == null)
            {
                return;
            }

            var user = userService.GetUser(member.UserId);
            try
            {
                var result = MessageBox.Show("Delete this account?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    userService.DeleteUser(user);

                    var jobApplieds = jobApplicationService
                        .GetJobApplications()
                        .Where(x => x.MemberId == member.MemberId);

                    foreach (var application in jobApplieds)
                    {
                        application.Status = "Member Deleted";
                        jobApplicationService.UpdateJobApplication(application);
                    }

                    var interviews = interviewScheduleService
                        .GetInterviewSchedules()
                        .Where(x => jobApplieds.Select(y => y.ApplicationId).Contains(x.ApplicationId));

                    foreach (var i in interviews)
                    {
                        i.Status = "Member Deleted";
                        interviewScheduleService.UpdateInterviewSchedule(i);
                    }

                    LoadManageMember();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void UpdateMember_Click(object sender, RoutedEventArgs e)
        {
            var member = ManageMemberDataGrid.SelectedItem as Member;

            if (member == null)
            {
                return;
            }

            UpdateMemberWindow updateMemberWindow = new UpdateMemberWindow(member);
            updateMemberWindow.ShowDialog();
            LoadManageMember();
        }

        private void ViewDeletedMembers_Click(object sender, RoutedEventArgs e)
        {
            ViewDeletedMembersWindow viewDeletedMembersWindow = new ViewDeletedMembersWindow();
            viewDeletedMembersWindow.ShowDialog();
        }
    }
}
