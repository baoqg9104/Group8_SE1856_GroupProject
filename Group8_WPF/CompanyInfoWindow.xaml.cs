using BusinessObjects;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for CompanyInfoWindow.xaml
    /// </summary>
    public partial class CompanyInfoWindow : Window
    {
        public CompanyInfoWindow(RecruitmentCompany company) // Pass the company object
        {
            InitializeComponent();  
            DataContext = company; // Set the DataContext to bind the UI
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window
        }
    }
}
