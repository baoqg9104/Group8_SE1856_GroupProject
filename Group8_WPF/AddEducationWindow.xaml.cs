using BusinessObjects;
using DataAccessLayers.DTOs.Education;
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
    /// Interaction logic for AddEducationWindow.xaml
    /// </summary>
    public partial class AddEducationWindow : Window
    {
        private readonly IEducationService educationService;
        private readonly IUserService userService;

        public AddEducationWindow()
        {
            InitializeComponent();
            educationService = new EducationService();
            userService = new UserService();

            var fromYears = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).Reverse().ToList();
            var toYears = Enumerable.Range(2000, DateTime.Now.Year + 4 - 2000 + 1).Reverse().ToList();
            FromYearComboBox.ItemsSource = fromYears;
            ToYearComboBox.ItemsSource = toYears;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var school = SchoolTextBox.Text;
                var major = MajorTextBox.Text;
                var isCurrent = IsCurrent.IsChecked ?? false;
                var fromMonth = FromMonthComboBox.Text == "" ? 0 : int.Parse(FromMonthComboBox.Text);
                var fromYear = FromYearComboBox.Text == "" ? 0 : int.Parse(FromYearComboBox.Text);
                var toMonth = ToMonthComboBox.Text == "" ? 0 : int.Parse(ToMonthComboBox.Text);
                var toYear = ToYearComboBox.Text == "" ? 0 : int.Parse(ToYearComboBox.Text);

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var member = userService.GetUser(userId).Member;

                var addEducation = new Education()
                {
                    MemberId = member.MemberId,
                    School = school,
                    Major = major,
                    IsCurrent = isCurrent,
                    FromMonth = fromMonth,
                    FromYear = fromYear,
                    ToMonth = toMonth,
                    ToYear = toYear,
                };

                educationService.AddEducation(addEducation);
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow?.LoadEducations();
                MessageBox.Show("Add education successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IsCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (IsCurrent.IsChecked == true)
            {
                ToMonthComboBox.Text = string.Empty;
                ToYearComboBox.Text = string.Empty;
                ToMonthComboBox.IsEnabled = false;
                ToYearComboBox.IsEnabled = false;
            } else
            {
                ToMonthComboBox.IsEnabled = true;
                ToYearComboBox.IsEnabled = true;
            }
        }
    }
}
