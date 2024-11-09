using DataAccessLayers;
using Microsoft.Extensions.Configuration;
using Services;
using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        private readonly IUserService userService;

        public SignupWindow()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private void HandleSignup(object sender, RoutedEventArgs e)
        {
            string fullName = FullName.Text;
            string email = Email.Text;
            string password = Password.Password;

            try
            {
                userService.Signup(fullName, email, password);
                MessageBox.Show("Sign up successful");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleSignup(sender, e);
            }
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
            this.Close();
        }
    }
}
